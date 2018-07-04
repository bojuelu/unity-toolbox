using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AliyunSDK;

public class UsingAliyunOss : MonoBehaviour
{
    public OssAgent ossAgent;

    double t1;
    double t2;

    public void Start()
    {
        ossAgent.Init("oss-cn-beijing.aliyuncs.com", "LTAIQVyKVKUYgON8", "fzima0CbNZPtbRE6EX9Tv4G6A2aYl9", "makar");
        ossAgent.onPutObjectDone += OnUploadDone;
    }

    void OnDestroy()
    {
        ossAgent.onPutObjectDone -= OnUploadDone;
    }

    public void UploadFile()
    {
        t1 = System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1)).TotalSeconds;
        ossAgent.PutObjectByNewThread("/Users/haro/Downloads/test_large_file.txt", System.DateTime.Now.ToString() + ".file");
    }

    private void OnUploadDone(OssAgent sender, string url)
    {
        t2 = System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1)).TotalSeconds;
        Debug.Log("OnUploadDone");
        Debug.Log("Spend=" + (t2 - t1));
        Debug.Log("Download url=" + url);
    }
}
