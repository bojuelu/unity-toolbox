using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace UnityToolbox
{
    /// <summary>
    /// Download any data from web, save into local cache.
    /// Next time, use same url will download from local cache instead from web.
    /// </summary>
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

        private static string cacheTableName = "cache_table_201804261948";

        //private static bool checkedEncryptionVersion = false;

        /// <summary>
        /// Give a web url, download it and save to disk.
        /// Next time use the same url, it will download from local disk.
        /// Callback can get a WWW object, orignal web URL, and save to local disk path
        /// </summary>
        /// <param name="downloadURL">URL.</param>
        /// <param name="callback">Callback function.</param>
        /// <param name="md5CheckSum">Md5 check sum.</param>
        /// <param name="readModel">use 0 is old save model , use  1 is  new save model, if you use 1 model md5 related null</param>
        public void Download(
            string downloadURL,
            DownloadCompleteHandler callback,
            bool saveToPersistDataPath = false,
            string md5Checksum = null,
            System.Text.Encoding md5ChecksumEncoding = null,
            int readModel = 0
        )
        {
            if (isDownloading)
            {
                Debug.LogError("[DataDownloader] download coroutine not complete yet, try again later");
                return;
            }
            else if (!IsValidURL(downloadURL))
            {
                Debug.LogError("[DataDownloader] download URL is invalid, the URL:" + downloadURL);
                return;
            }

            url = downloadURL;

            if(readModel == 0)
            {
                this.StartCoroutine(this.DownloadCoroutine(url, callback, saveToPersistDataPath, md5Checksum, md5ChecksumEncoding));
                isDownloading = true;
            }
            else if(readModel == 1)
            {
                this.StartCoroutine(this.DownloadCoroutineEncrypt(url, callback, saveToPersistDataPath));
                isDownloading = true;
            }
            else
            {
                Debug.LogError("[DataDownloader] readModel not support. readModel=" + readModel.ToString());
                isDownloading = false;
            }
        }

        public static IDictionary<string, string> CacheTable()
        {
            return GetCacheTable();
        }

        private static string GetCacheFilesLocation(bool saveToPersistDataPath)
        {
            if (saveToPersistDataPath)
                return System.IO.Path.Combine(Application.persistentDataPath, ("data_downloader" + System.IO.Path.DirectorySeparatorChar));
            else
                return System.IO.Path.Combine(Application.temporaryCachePath, ("data_downloader" + System.IO.Path.DirectorySeparatorChar));
        }

        private static string GetCacheTableLocation()
        {
            return GetCacheFilesLocation(true);            
        }

        private static Dictionary<string, string> GetCacheTable()
        {
            string cacheTableLocation = GetCacheTableLocation();

            if (!UnityUtility.IsDirectoryExist(cacheTableLocation))
                UnityUtility.CreateDirectory(cacheTableLocation);

            //if (checkedEncryptionVersion == false)
            //{
            //    float lastVersion = PlayerPrefs.GetFloat("EncryptionHelper.Version", 0f);
            //    float nowVersion = EncryptionHelper.Version;
            //    bool needClearCacheTable = false;

            //    Debug.Log(string.Format("[DataDownloader] EncryptionHelper.Version now:{0} , last:{1}", nowVersion, lastVersion));

            //    if (lastVersion != nowVersion)
            //    {
            //        needClearCacheTable = true;
            //    }
            //    PlayerPrefs.SetFloat("EncryptionHelper.Version", nowVersion);
            //    checkedEncryptionVersion = true;

            //    if (needClearCacheTable)
            //    {
            //        Debug.LogWarning("[DataDownloader] Clear cache table bcz EncryptionHelper.Version is not the same");
            //        Dictionary<string, string> emptyCacheTable = new Dictionary<string, string>();
            //        WriteCacheTable(emptyCacheTable);
            //        return emptyCacheTable;
            //    }
            //}

            if (!UnityUtility.IsFileExist(cacheTableName, cacheTableLocation))
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                Debug.Log("[DataDownloader] no cache table exist, will create a new one");
                WriteCacheTable(dic);
                return dic;
            }
            else
            {
                string strOrigData = UnityUtility.ReadTextFile(cacheTableName, cacheTableLocation, EncryptionHelper.Encoding);
                string strJsonData = UnityUtility.RotCypher(strOrigData);
                JSONObject json = new JSONObject(strJsonData);
                return json.ToDictionary();
            }
        }

        private static string WriteCacheTable(Dictionary<string, string> dic)
        {
            JSONObject json = new JSONObject(dic);
            string strJson = json.Print();
            string strJsonEncrypt = UnityUtility.RotCypher(strJson);
            return UnityUtility.WriteTextFile(strJsonEncrypt, EncryptionHelper.Encoding, cacheTableName, GetCacheTableLocation());
        }

        private static string UniqueFileName()
        {
            string id = string.Format(
                "cache{0}{1}",
                (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                UnityUtility.GenerateRandomStringViaCharacter(8)
            );
            return id;
        }

        private IEnumerator DownloadCoroutine(
            string downloadURL,
            DownloadCompleteHandler callback,
            bool saveToPersistDataPath,
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
            downloadProgress = 0f;
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
                Debug.LogError("[DataDownloader] " + wwwObj.error);

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
                string willSaveCacheLoc = GetCacheFilesLocation(saveToPersistDataPath);
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
                    Debug.LogWarning("[DataDownloader] " + willSaveCacheFullPath + " existed, orignal file will be delete");
                    UnityUtility.DeleteFile(willSaveCacheFullPath);
                }

                // save file to local disk
                savedCachePath = UnityUtility.WriteFile(wwwObj.bytes, willSaveCacheFullPath);
                // file IO operation error occur, callback and break this coroutine
                if (string.IsNullOrEmpty(savedCachePath))
                {
                    Debug.LogError("[DataDownloader] save file " + savedCachePath + " failed");
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
                    Debug.LogError("[DataDownloader] write cache table " + updatedCacheTableFullPath + " failed");
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

        private IEnumerator DownloadCoroutineEncrypt (
            string downloadURL,
            DownloadCompleteHandler callback,
            bool saveToPersistDataPath
        )
        {
           
            
            if (wwwObj != null)
            {
                wwwObj.Dispose();
                wwwObj = null;
            }

           

             // check if it can download from cache
            bool isDownloadFromCache = false;
            string aesEncryptUrl = AESEncryptionHelper.Encrypt(downloadURL,0);
		    byte[] urlByte = Encoding.UTF8.GetBytes(aesEncryptUrl);
		    string Base64url = Convert.ToBase64String(urlByte);

            string willSaveCacheLoc = GetCacheFilesLocation(saveToPersistDataPath);
            string willSaveCacheFullPath = willSaveCacheLoc + Base64url;
            string willSaveCacheFullPathCache = willSaveCacheFullPath + "Cache";

            if(UnityUtility.IsFileExist(willSaveCacheFullPath))
            {
                isDownloadFromCache = true;
            }

             // download data from local cache
            if (isDownloadFromCache)
            {
                string localObjText = UnityUtility.ReadTextFile(willSaveCacheFullPath,Encoding.UTF8);
                string localObjAesDecrypt = AESEncryptionHelper.Decrypt(localObjText,0);
                byte[] ocalObj = Convert.FromBase64String(localObjAesDecrypt);
                
                UnityUtility.WriteFile(ocalObj,willSaveCacheFullPathCache);
                wwwObj = new WWW(UnityUtility.FilePathToFileURL(willSaveCacheFullPathCache));
            }
            // download data from web url
            else
            {

                wwwObj = new WWW(downloadURL);
            }

             // wait until download complete
            downloadProgress = 0f;
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
                Debug.LogError("[DataDownloader] " + wwwObj.error);

                if (callback != null)
                    callback(wwwObj, downloadURL, "");
                yield break;
            }

              // if it is downloaded from cache, not need to save to cache again. invoke callback and break this coroutine
            if (isDownloadFromCache)
            {
                if (callback != null)
                {
                    callback(wwwObj, downloadURL, willSaveCacheFullPath);
                    UnityUtility.DeleteFile(willSaveCacheFullPathCache);
                }
                yield break;
            }
            else
            {
              
                string wwwObjBase64 = Convert.ToBase64String(wwwObj.bytes);
                string wwwObjAesEncrypt = AESEncryptionHelper.Encrypt(wwwObjBase64,0);
                
                // save file to local disk
                string savedCache = UnityUtility.WriteTextFile(wwwObjAesEncrypt,Encoding.UTF8,willSaveCacheFullPath);

                // file IO operation error occur, callback and break this coroutine
                if (string.IsNullOrEmpty(savedCache))
                {
                    Debug.LogError("[DataDownloader] save file " + savedCache + " failed");
                    if (callback != null)
                        callback(wwwObj, downloadURL, "");
                    yield break;
                }

                // callback and end this coroutine
                if (callback != null)
                {
                    callback(wwwObj, downloadURL, willSaveCacheFullPath);
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
                    Download(url, null);
                else
                    Debug.LogError("[DataDownloader] download URL is invalid, the URL:" + url);
            }
        }

        private void OnDestroy()
        {
            if (isDownloading)
            {
                Debug.LogWarning("[DataDownloader] DownloadCoroutine has not finished, it will be terminated");
                this.StopAllCoroutines();
            }
            if (wwwObj != null)
            {
                wwwObj.Dispose();
                wwwObj = null;
            }
        }
    }
}
