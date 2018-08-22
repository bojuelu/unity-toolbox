using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using MiniJSON;

namespace UnityToolbox
{
    /// <summary>
    /// File downloader.
    /// Call Download() for download anything. and it will save to disk, next time the same url will load from disk directly.
    /// Call 
    /// </summary>
    public class FileDownloader : MonoBehaviour
    {
        public delegate void DownloadCompleteHandler(FileDownloader sender, string url, byte[] fileData);
        public delegate void DownloadProgressHandler(FileDownloader sender, string url, float progress);

        private static Dictionary<string, string> cacheTable = null;
        public static IDictionary<string, string> CacheTable { get { return cacheTable; } }

        public void Download(string url, DownloadProgressHandler progressHandler, DownloadCompleteHandler completeHandler)
        {
            StartCoroutine(DownloadCoroutine(url, progressHandler, completeHandler));
        }

        public void SaveCacheTable()
        {
            string cacheTableFolderPath = GetCacheTableFileFolder();

            if (!Directory.Exists(cacheTableFolderPath))
            {
                Directory.CreateDirectory(cacheTableFolderPath);
            }
            string cacheTableFilePath = GetCacheTableFilePath();

            string jsonStr = Json.Serialize(cacheTable);
            File.WriteAllText(cacheTableFilePath, jsonStr);
        }

        void LoadCacheTable()
        {
            string cacheTableFolderPath = GetCacheTableFileFolder();

            if (!Directory.Exists(cacheTableFolderPath))
            {
                Directory.CreateDirectory(cacheTableFolderPath);
            }
            string cacheTableFilePath = GetCacheTableFilePath();

            if (File.Exists(cacheTableFilePath))
            {
                string jsonStr = File.ReadAllText(cacheTableFilePath, System.Text.Encoding.UTF8);

                Dictionary<string, object> dict = Json.Deserialize(jsonStr) as Dictionary<string, object>;
                cacheTable = new Dictionary<string, string>();
                foreach (KeyValuePair<string, object> kvp in dict)
                {
                    cacheTable.Add(kvp.Key, kvp.Value as string);
                }
                return;
            }
            else
            {
                File.WriteAllText(cacheTableFilePath, "{}");
                cacheTable = new Dictionary<string, string>();
                return;
            }
        }

        IEnumerator DownloadCoroutine(string url, DownloadProgressHandler progressHandler, DownloadCompleteHandler completeHandler)
        {
            yield return null;

            if (cacheTable == null)
                LoadCacheTable();

            bool isDataDownloadedBefore = false;
            string localPath = null;
            if (cacheTable.ContainsKey(GetUrlKey(url)))
            {
                localPath = cacheTable[GetUrlKey(url)];
                if (File.Exists(localPath))
                {
                    isDataDownloadedBefore = true;
                }
                else
                {
                    cacheTable.Remove(GetUrlKey(url));
                }
            }

            if (isDataDownloadedBefore)
            {
                byte[] fileData = File.ReadAllBytes(localPath);
                if (completeHandler != null)
                {
                    completeHandler(this, url, fileData);
                }
            }
            else
            {
                using (UnityWebRequest www = UnityWebRequest.Get(url))
                {
                    www.Send();
                    while (!www.isDone)
                    {
                        if (progressHandler != null)
                        {
                            progressHandler(this, url, www.downloadProgress);
                        }
                        yield return null;
                    }
                    if (www.responseCode != 200)
                    {
                        Debug.LogError("www.responseCode=" + www.responseCode);
                        if (completeHandler != null)
                        {
                            completeHandler(this, url, null);
                        }
                        yield break;
                    }

                    if (www.downloadHandler.data == null)
                    {
                        Debug.LogError("www.downloadHandler.data is null");
                        if (completeHandler != null)
                        {
                            completeHandler(this, url, null);
                        }
                        yield break;
                    }

                    byte[] fileData = new byte[www.downloadHandler.data.Length];
                    try
                    {
                        Array.Copy(www.downloadHandler.data, fileData, fileData.Length);
                    }
                    catch (Exception exc)
                    {
                        Debug.LogException(exc);
                        if (completeHandler != null)
                        {
                            completeHandler(this, url, null);
                        }
                        yield break;
                    }

                    string newFileName = GenerateFileName();
                    string saveFolder = Path.Combine(Application.persistentDataPath, "FileDownloader");

                    if (!Directory.Exists(saveFolder))
                    {
                        try
                        {
                            Directory.CreateDirectory(saveFolder);
                        }
                        catch (IOException exc)
                        {
                            Debug.LogException(exc);
                            if (completeHandler != null)
                            {
                                completeHandler(this, url, null);
                            }
                            yield break;
                        }
                    }
                    string savePath = Path.Combine(saveFolder, newFileName);
                    bool saveSuccess = false;

                    try
                    {
                        File.WriteAllBytes(savePath, fileData);
                        saveSuccess = true;
                    }
                    catch (IOException exc)
                    {
                        Debug.LogException(exc);
                        saveSuccess = false;
                    }

                    if (saveSuccess)
                    {
                        cacheTable.Add(GetUrlKey(url), savePath);
                        if (completeHandler != null)
                        {
                            completeHandler(this, url, fileData);
                        }
                    }
                    else
                    {
                        Debug.LogError("file save failed.");
                        if (completeHandler != null)
                        {
                            completeHandler(this, url, null);
                        }
                    }
                }
            }
        }

        string GetUrlKey(string url)
        {
            return "FileDownloader:" + url;
        }

        string GenerateFileName()
        {
            return string.Format("{0}.{1}.file", DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds, UnityEngine.Random.Range(1000, 9999));
        }

        string GetCacheTableFileFolder()
        {
            return Path.Combine(Application.persistentDataPath, "FileDownloader");
        }

        string GetCacheTableFilePath()
        {
            return Path.Combine(GetCacheTableFileFolder(), "aebbfom");
        }

        void OnDestroy()
        {
            SaveCacheTable();
        }
    }
}
