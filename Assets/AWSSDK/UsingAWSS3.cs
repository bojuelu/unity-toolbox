using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingAWSS3 : MonoBehaviour
{
    public S3Function weidunS3;

    void Start()
    {
        weidunS3.setIdentityPoolId("ab-cdefghijk-l:12345678-1234-1234-1234-123456789012");
        weidunS3.setCognitoIdentityRegion("ab-cdefghijk-l");
        weidunS3.setS3Region("ab-cdefghijk-l");
        weidunS3.setBucketName("aaaa.test.accessable");
    }

    public void PostObj()
    {
        string postObj = "/Users/yuyuhashow/Downloads/0615/testyo.txt";
        Debug.Log("post obj:" + postObj);
        Debug.Log("file exists? =" + System.IO.File.Exists(postObj));
        Debug.Log(
            string.Format(
                "{0}\n{1}\n{2}\n{3}",
                weidunS3.GetIdentityPoolId,
                weidunS3.GetCognitoIdentityRegion,
                weidunS3.GetS3Region,
                weidunS3.GetS3BucketName
            )
        );
        weidunS3.PostObject("ABC/", postObj, "file1.txt", (string strResult) =>
            {
                Debug.Log(strResult);
            });
    }
}
