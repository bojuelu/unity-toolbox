using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DataDownloader : MonoBehaviour
{
    public bool autoStart = true;
    public string url = "";

    private bool isDownloading = false;
    public bool IsDownloading { get { return isDownloading; } }

    private WWW wwwObj = null;
    private float downloadProgress = 0f;
    public float DownloadProgress { get { return downloadProgress; } }

    public delegate void DownloadCompleteHandler(WWW www, string cacheFilePath);

    private static string cacheTableName = "cache_table.txt";

    private static string CacheDataLoc()
    {
        return UnityUtility.PathFormat(Application.temporaryCachePath) + "data_downloader" + System.IO.Path.DirectorySeparatorChar;
    }

    /// <summary>
    /// Give a web url, download it and save to disk.
    /// Next time use the same url, it will download from local disk.
    /// Callback can get a WWW object, and save to local disk path
    /// </summary>
    /// <param name="url">URL.</param>
    /// <param name="callback">Callback.</param>
    /// <param name="md5CheckSum">Md5 check sum.</param>
    public void Download(string url, DownloadCompleteHandler callback, string md5Checksum = "")
    {
        // Avoid double calling this function while the other MonoBehavior use AddComponent and call this function immediately
        this.autoStart = false;

        if (isDownloading)
        {
            Debug.LogError("Download coroutine not complete yet, try again later");
            return;
        }

        // check if it can download from cache
        bool downloadFromCache = false;
        string cacheFilePath = "";
        Dictionary<string, string> cacheTable = ReadCacheTable();
        if (cacheTable.ContainsKey(url))
        {
            cacheFilePath = cacheTable[url];
            if (UnityUtility.IsFileExist(cacheFilePath))
            {
                if (!string.IsNullOrEmpty(md5Checksum))
                {
                    if (EncryptionHelper.MD5ChecksumCode(cacheFilePath) == md5Checksum)
                        downloadFromCache = true;
                }
                else
                {
                    downloadFromCache = true;
                }
            }
        }

        // download from local cache
        if (downloadFromCache)
        {
            this.StartCoroutine(this.DownloadCoroutine(UnityUtility.LocalURL(cacheFilePath), "", "", callback));
        }
        // download from web and save to cache
        else
        {
            this.StartCoroutine(this.DownloadCoroutine(url, UniqueFileName(), CacheDataLoc(), callback));
        }
    }

    private IEnumerator DownloadCoroutine(string url, string saveFileName, string saveFileLoc, DownloadCompleteHandler callback)
    {
        if (wwwObj != null)
        {
            wwwObj.Dispose();
            wwwObj = null;
        }
        Debug.Log(string.Format(
            "download data from:{0} saveFileName: {1} saveFileLoc: {2}", url, saveFileName, saveFileLoc)
        );

        isDownloading = true;
        wwwObj = new WWW(url);
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
                callback.Invoke(wwwObj, "");
            yield break;
        }

        // if it is not need to save file, means url is a local file path, callback and break this coroutine
        if (string.IsNullOrEmpty(saveFileName))
        {
            if (callback != null)
                callback.Invoke(wwwObj, UnityUtility.LocalURLToFilePath(url));
            yield break;
        }

        // add file extention to file name
        string extName = ".asset";
        if (UnityUtility.IsJPG(wwwObj))
            extName = ".jpg";
        else if (UnityUtility.IsPNG(wwwObj))
            extName = ".png";
        else if (UnityUtility.IsGIF(wwwObj))
            extName = ".gif";
        else if (UnityUtility.IsMP4(wwwObj))
            extName = ".mp4";
        saveFileName += extName;

        // check if saved before, if yes, delete old one
        string saveFilePath = saveFileLoc + saveFileName;
        if (UnityUtility.IsFileExist(saveFilePath))
        {
            Debug.LogWarning(saveFilePath + " existed, file will be delete");
            UnityUtility.DeleteFile(saveFilePath);
        }

        // save file to local disk
        string saveFileCompletedPath = UnityUtility.WriteFile(saveFilePath, wwwObj.bytes);
        // file IO operation error occur, callback and break this coroutine
        if (string.IsNullOrEmpty(saveFileCompletedPath))
        {
            Debug.LogError("save file " + saveFileCompletedPath + " failed");
            if (callback != null)
                callback.Invoke(wwwObj, "");
            yield break;
        }

        // update cache table
        Dictionary<string, string> cacheTable = ReadCacheTable();
        if (cacheTable.ContainsKey(url))
        {
            cacheTable.Remove(url);
        }
        cacheTable.Add(url, saveFileCompletedPath);
        WriteCacheTable(cacheTable);

        // callback and end this coroutine
        if (callback != null)
        {
            callback.Invoke(wwwObj, saveFileCompletedPath);
        }
    }

    public static Dictionary<string, string> GetCacheTable()
    {
        return ReadCacheTable();
    }

    public static bool OverwriteCacheTable(Dictionary<string, string> dic)
    {
        if (!UnityUtility.IsDirectoryExist(CacheDataLoc()))
            return false;
        else if (!UnityUtility.IsFileExist(cacheTableName, CacheDataLoc()))
            return false;

        string writeLoc = WriteCacheTable(dic);
        if (string.IsNullOrEmpty(writeLoc))
            return false;
        else
            return true;
    }

    private static Dictionary<string, string> ReadCacheTable()
    {
        if (!UnityUtility.IsDirectoryExist(CacheDataLoc()))
        {
            UnityUtility.CreateDirectory(CacheDataLoc());
        }
        if (!UnityUtility.IsFileExist(cacheTableName, CacheDataLoc()))
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Debug.Log("no cache table exist, will create a new one");
            WriteCacheTable(dic);
        }

        string strOrigData = UnityUtility.ReadTextFile(cacheTableName, CacheDataLoc(), EncryptionHelper.Encode);
        string strJsonData = EncryptionHelper.Decrypt(strOrigData);
        JSONObject json = new JSONObject(strJsonData);
        return json.ToDictionary();
    }

    private static string WriteCacheTable(Dictionary<string, string> dic)
    {
        JSONObject json = new JSONObject(dic);
        string strJson = json.Print(true);
        Debug.Log("will write cache table: " + strJson);
        string strJsonEncrypt = EncryptionHelper.Encrypt(strJson);
        byte[] byteData = EncryptionHelper.GetBytes(strJsonEncrypt);
        return UnityUtility.WriteFile(cacheTableName, byteData, CacheDataLoc());
    }

    private static string UniqueFileName()
    {
        string id = string.Format(
            "cache{0}{1}",
            DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
            UnityUtility.GenerateRandomString(8)
        );
        return id;
    }

    private void Start()
    {
        if (autoStart)
        {
            if (url.Contains("http://") || url.Contains("https://"))
                Download(url, null, "");
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
