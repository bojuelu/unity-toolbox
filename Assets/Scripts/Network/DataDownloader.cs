using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DataDownloader : MonoBehaviour
{
    public bool autoStart = false;
    public string url = "";

    private bool isDownloading = false;
    public bool IsDownloading { get { return isDownloading; } }

    private WWW wwwObj = null;
    private float downloadProgress = 0f;
    public float DownloadProgress { get { return downloadProgress; } }

    public delegate void DownloadCompleteHandler(WWW www, string webURL, string savedCachePath);

    private static string cacheTableName = "cache_table.txt";

    private static bool checkedEncryptionVersion = false;

    /// <summary>
    /// Give a web url, download it and save to disk.
    /// Next time use the same url, it will download from local disk.
    /// Callback can get a WWW object, orignal web URL, and save to local disk path
    /// </summary>
    /// <param name="downloadURL">URL.</param>
    /// <param name="callback">Callback function.</param>
    /// <param name="md5CheckSum">Md5 check sum.</param>
    public void Download(
        string downloadURL,
        DownloadCompleteHandler callback,
        string md5Checksum = null,
        System.Text.Encoding md5ChecksumEncoding = null
    )
    {
        if (isDownloading)
        {
            Debug.LogError("download coroutine not complete yet, try again later");
            return;
        }
        else if (!IsValidURL(downloadURL))
        {
            Debug.LogError("download URL is invalid, the URL:" + downloadURL);
            return;
        }

        url = downloadURL;

        this.StartCoroutine(this.DownloadCoroutine(url, callback, md5Checksum, md5ChecksumEncoding));
        isDownloading = true;
    }

    public static IDictionary<string, string> CacheTable()
    {
        return GetCacheTable();
    }

    public static string CacheFilesLocation()
    {
        return GetCacheFilesLocation();
    }

    private static string GetCacheFilesLocation()
    {
        return System.IO.Path.Combine(Application.temporaryCachePath, ("data_downloader" + System.IO.Path.DirectorySeparatorChar));
    }

    private static Dictionary<string, string> GetCacheTable()
    {        
        string cacheFilesLocation = GetCacheFilesLocation();

        if (!UnityUtility.IsDirectoryExist(cacheFilesLocation))
            UnityUtility.CreateDirectory(cacheFilesLocation);

        if (checkedEncryptionVersion == false)
        {
            float lastVersion = PlayerPrefs.GetFloat("EncryptionHelper.Version", 0f);
            float nowVersion = EncryptionHelper.Version;
            bool needClearCacheTable = false;

            Debug.Log(string.Format("EncryptionHelper.Version now:{0} , last:{1}", nowVersion, lastVersion));

            if (lastVersion != nowVersion)
            {
                needClearCacheTable = true;
            }
            PlayerPrefs.SetFloat("EncryptionHelper.Version", nowVersion);
            checkedEncryptionVersion = true;

            if (needClearCacheTable)
            {
                Debug.LogWarning("Clear cache table bcz EncryptionHelper.Version is not the same");
                Dictionary<string, string> emptyCacheTable = new Dictionary<string, string>();
                WriteCacheTable(emptyCacheTable);
                return emptyCacheTable;
            }
        }

        if (!UnityUtility.IsFileExist(cacheTableName, cacheFilesLocation))
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Debug.Log("no cache table exist, will create a new one");
            WriteCacheTable(dic);
            return dic;
        }
        else
        {
            string strOrigData = UnityUtility.ReadTextFile(cacheTableName, cacheFilesLocation, EncryptionHelper.Encoding);
            string strJsonData = EncryptionHelper.Decrypt(strOrigData, 0);
            JSONObject json = new JSONObject(strJsonData);
            return json.ToDictionary();
        }
    }

    private static string WriteCacheTable(Dictionary<string, string> dic)
    {
        JSONObject json = new JSONObject(dic);
        string strJson = json.Print();
        string strJsonEncrypt = EncryptionHelper.Encrypt(strJson, 0);
        return UnityUtility.WriteTextFile(strJsonEncrypt, EncryptionHelper.Encoding, cacheTableName, GetCacheFilesLocation());
    }

    private static string UniqueFileName()
    {
        string id = string.Format(
            "cache{0}{1}",
            (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
            UnityUtility.GenerateRandomString(8)
        );
        return id;
    }

    private IEnumerator DownloadCoroutine(
        string downloadURL,
        DownloadCompleteHandler callback,
        string md5Checksum,
        System.Text.Encoding md5ChecksumEncoding
    )
    {
        if (wwwObj != null)
        {
            wwwObj.Dispose();
            wwwObj = null;
        }

        // check if it can download from cache
        bool isDownloadFromCache = false;
        string savedCachePath = "";
        Dictionary<string, string> cacheTable = GetCacheTable();
        // if url exist in the table, it might download from cache
        if (cacheTable.ContainsKey(downloadURL))
        {
            // check if cache file is really existed
            savedCachePath = cacheTable[downloadURL];
            if (UnityUtility.IsFileExist(savedCachePath))
            {
                // if this function caller give a md5 checksum code, go check it
                if (!string.IsNullOrEmpty(md5Checksum))
                {
                    if (EncryptionHelper.MD5ChecksumCode(md5ChecksumEncoding, savedCachePath) == md5Checksum)
                    {
                        // pass checksum, download from cache
                        isDownloadFromCache = true;
                    }
                }
                else
                {
                    // has no md5 checksum code, and cache file is really existed, download from cache
                    isDownloadFromCache = true;
                }
            }
        }

        // download data from local cache
        if (isDownloadFromCache)
        {
            wwwObj = new WWW(UnityUtility.FilePathToFileURL(savedCachePath));
        }
        // download data from web url
        else
        {
            wwwObj = new WWW(downloadURL);
        }

        // wait until download complete
        yield return null;
        while (!wwwObj.isDone)
        {
            downloadProgress = wwwObj.progress;
            yield return null;
        }
        yield return wwwObj;
        downloadProgress = 1f;
        isDownloading = false;

        // if someting wrong, print error log and break this coroutine
        if (!string.IsNullOrEmpty(wwwObj.error))
        {
            Debug.LogError(wwwObj.error);

            if (callback != null)
                callback(wwwObj, downloadURL, "");
            yield break;
        }

        // if it is downloaded from cache, not need to save to cache again. invoke callback and break this coroutine
        if (isDownloadFromCache)
        {
            if (callback != null)
                callback(wwwObj, downloadURL, savedCachePath);
            yield break;
        }
        // it is download from web, ready to save cache file
        else
        {
            string willSaveCacheLoc = GetCacheFilesLocation();
            string willSaveCacheName = UniqueFileName();

            // check file type and add extent file name
            string extName = ".asset";
            if (UnityUtility.IsJPG(wwwObj))
                extName = ".jpg";
            else if (UnityUtility.IsPNG(wwwObj))
                extName = ".png";
            else if (UnityUtility.IsGIF(wwwObj))
                extName = ".gif";
            else if (UnityUtility.IsMP4(wwwObj))
                extName = ".mp4";
            willSaveCacheName += extName;

            // build the cache file full path
            string willSaveCacheFullPath = willSaveCacheLoc + willSaveCacheName;

            // check if saved before, if yes, delete old one
            if (UnityUtility.IsFileExist(willSaveCacheFullPath))
            {
                Debug.LogWarning(willSaveCacheFullPath + " existed, orignal file will be delete");
                UnityUtility.DeleteFile(willSaveCacheFullPath);
            }

            // save file to local disk
            savedCachePath = UnityUtility.WriteFile(wwwObj.bytes, willSaveCacheFullPath);
            // file IO operation error occur, callback and break this coroutine
            if (string.IsNullOrEmpty(savedCachePath))
            {
                Debug.LogError("save file " + savedCachePath + " failed");
                if (callback != null)
                    callback(wwwObj, downloadURL, "");
                yield break;
            }

            // update cache table
            cacheTable = GetCacheTable();
            if (cacheTable.ContainsKey(downloadURL))
            {
                cacheTable.Remove(downloadURL);
            }
            cacheTable.Add(downloadURL, savedCachePath);
            string updatedCacheTableFullPath = WriteCacheTable(cacheTable);
            // file IO operation error occur, callback and break this coroutine
            if (string.IsNullOrEmpty(updatedCacheTableFullPath))
            {
                Debug.LogError("write cache table " + updatedCacheTableFullPath + " failed");
                if (callback != null)
                    callback(wwwObj, downloadURL, "");
                yield break;
            }

            // callback and end this coroutine
            if (callback != null)
            {
                callback(wwwObj, downloadURL, savedCachePath);
            }
            yield break;
        }
    }

    private bool IsValidURL(string s)
    {
        if (string.IsNullOrEmpty(s))
            return false;
        else if (!s.Contains("http://") && !s.Contains("https://"))
            return false;
        else
            return true;
    }

    private void Start()
    {
        if (autoStart && !isDownloading)
        {
            if (IsValidURL(url))
                Download(url, null, "");
            else
                Debug.LogError("download URL is invalid, the URL:" + url);
        }
    }

    private void OnDestroy()
    {
        if (isDownloading)
        {
            Debug.LogWarning("DownloadCoroutine has not finished, it will be terminated");
            this.StopAllCoroutines();
        }
        if (wwwObj != null)
        {
            wwwObj.Dispose();
            wwwObj = null;
        }
    }
}
