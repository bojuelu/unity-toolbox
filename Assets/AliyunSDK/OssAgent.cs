using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System;

using UnityEngine;

using Aliyun.OSS;
using Aliyun.OSS.Util;

namespace AliyunSDK
{
    /// <summary>
    /// Oss agent.
    /// Use this script to upload file to Aliyun oss.
    /// 
    /// Heed! call Init() before any other function called.
    /// 
    /// Refrence: https://help.aliyun.com/document_detail/32085.html
    ///           https://www.cnblogs.com/doforfuture/p/6293926.html
    /// 
    /// Powered by: https://github.com/Shirlman/aliyun-oss-unity-sdk.git
    /// Author: BoJue
    /// </summary>
    public class OssAgent : MonoBehaviour
    {
        public string endpoint = "abc-de-fghijkl.mnopqrst.uvw";
        public string accessKeyId = "1234567890";
        public string accessKeySecret = "12345678901234567890";
        public string bucketName = "abcdefg";

        public delegate void PutObjectDoneHandler(string url);
        public event PutObjectDoneHandler onPutObjectDone;

        OssClient client = null;

        public void Init()
        {
            Init("", "", "", "");
        }

        public void Init(string endpoint="", string accessKeyId="", string accessKeySecret="", string bucketName="")
        {
            if (!string.IsNullOrEmpty(endpoint))
                this.endpoint = endpoint;
            if (!string.IsNullOrEmpty(accessKeyId))
                this.accessKeyId = accessKeyId;
            if (!string.IsNullOrEmpty(accessKeySecret))
                this.accessKeySecret = accessKeySecret;
            if (!string.IsNullOrEmpty(bucketName))
                this.bucketName = bucketName;

            client = new OssClient(this.endpoint, this.accessKeyId, this.accessKeySecret);
        }

        Thread putObjectThread = null;
        string putObjectfilePath = "";
        string putObjectKey = "";
        bool putObjectIsPublicRead = false;
        public void PutObjectByNewThread(string filePath, string key, bool isPublicRead=true)
        {
            this.putObjectfilePath = filePath;
            this.putObjectKey = key;
            this.putObjectIsPublicRead = isPublicRead;

            if (putObjectThread != null)
                putObjectThread.Abort();
            putObjectThread = new Thread(PutObject);
            putObjectThread.Start();
        }

        private void PutObject()
        {
            PutObject(this.putObjectfilePath, this.putObjectKey, this.putObjectIsPublicRead);
        }

        public void PutObject(string filePath, string key, bool isPublicRead=true)  // I tried Async method, but still stuck main thread in Unity...
        {
            // avoid weird url
            key = key.Replace(":", "");
            key = key.Replace(" ", "");

            try
            {
                Debug.Log(string.Format("try PutObject start. filePath={0} key={1}", filePath, key));

                string fileToUpload = filePath;
                string md5 = "";

                using (var fs = File.Open(fileToUpload, FileMode.Open))
                {
                    md5 = OssUtils.ComputeContentMd5(fs, fs.Length);
                }
                var objectMeta = new ObjectMetadata
                    {
                        ContentMd5 = md5
                    };
                PutObjectResult result =  client.PutObject(bucketName, key, fileToUpload, objectMeta);
                Debug.Log("Put object succeeded");

                if (isPublicRead)
                {
                    Debug.Log("setup ACL = PublicRead start");

                    client.SetObjectAcl(bucketName, key, CannedAccessControlList.PublicRead);

                    Debug.Log("setup ACL = PublicRead end");

                    string url = string.Format("https://{0}.{1}/{2}", bucketName, endpoint, key);
                    if (onPutObjectDone != null)
                        onPutObjectDone(url);
                    return;
                }
                else
                {
                    if (onPutObjectDone != null)
                        onPutObjectDone(result.ETag);
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);

                if (onPutObjectDone != null)
                    onPutObjectDone("");
            }
            finally
            {
                Debug.Log("try PutObject end");
            }
        }

        void OnDisable()
        {
            if (putObjectThread != null)
            {
                Debug.Log("putObjectThread is not null, slopy to check thread state, Abort it anyway la.");

                putObjectThread.Abort();
            }
        }
    }
}