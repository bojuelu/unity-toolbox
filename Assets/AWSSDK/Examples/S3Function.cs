using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using System.IO;
using System;
using Amazon.S3.Util;
using System.Collections.Generic;
using Amazon.CognitoIdentity;
using Amazon;


///
/// Author: Weidun Wang (Adam)
///
public class S3Function : MonoBehaviour {

    private string IdentityPoolId = "";
    public string GetIdentityPoolId { get { return IdentityPoolId; } }
    private string CognitoIdentityRegion = RegionEndpoint.USEast1.SystemName;
    public string GetCognitoIdentityRegion { get { return CognitoIdentityRegion; } }
    private RegionEndpoint _CognitoIdentityRegion
    {
        get { return RegionEndpoint.GetBySystemName(CognitoIdentityRegion); }
    }
    private string S3Region = RegionEndpoint.USEast1.SystemName;
    public string GetS3Region { get { return S3Region; } }


    public delegate void void_Result_callback(string strResult);

    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }

    private string S3BucketName = null;
    public string GetS3BucketName { get { return S3BucketName; } }
//    public string SampleFileName = null;
//    public string SubFolder = null;

	// Use this for initialization
	void Start () {
        UnityInitializer.AttachToGameObject(this.gameObject);

	}

    public void setIdentityPoolId(string s)
    {
        IdentityPoolId = s;
    }

    public void setCognitoIdentityRegion(string s)
    {
        CognitoIdentityRegion = s;
    }

    public void setS3Region(string s)
    {
        S3Region = s;
    }

    public void setBucketName(string s)
    {
        S3BucketName = s;
    }



    #region private members
    private IAmazonS3 _s3Client;
    private AWSCredentials _credentials;

    private AWSCredentials Credentials
    {
        get
        { 
            if (_credentials == null)
                _credentials = new CognitoAWSCredentials(IdentityPoolId, _CognitoIdentityRegion);
            return _credentials;
        }
    }

    private IAmazonS3 Client
    {
        get
        {
            if (_s3Client == null)
            {
                _s3Client = new AmazonS3Client(Credentials, _S3Region);
            }
            return _s3Client;
        }
    }
    #endregion
    /// <summary>
    /// method to GetBucketList
    /// </summary>
    public string GetBucketList()
    {
        Debug.Log("Fetching all the Buckets");
        string strBucketList = null;
        Client.ListBucketsAsync(new ListBucketsRequest(), (responseObject) =>
        {
            if (responseObject.Exception == null)
            {
                Debug.Log("Get Response \n Printing now\n");
                responseObject.Response.Buckets.ForEach((s3b) =>
                {
                    strBucketList += string.Format("bucket = {0}, create date = {1} \n", s3b.BucketName, s3b.CreationDate);
                });
            }
            else
            {
                Debug.LogError("Got Exception \n");
            }
        });

        return strBucketList;
    }

    /// <summary>
    /// Get Object from S3 Bucket
    /// </summary>
    public void GetObject(string strSubFolder, string strFileName, void_Result_callback getObjectComplete = null)
    {
        string strGetObj = string.Format("fetching {0} from bucket {1}", strFileName, S3BucketName);
        Debug.Log("strGetObj:"+strGetObj);
        string strData = "";
        Client.GetObjectAsync(S3BucketName, strSubFolder + strFileName, (responseObj) =>
        {
            string data = null;
            var response = responseObj.Response;
            if (response.ResponseStream != null)
            {
                using(StreamReader reader = new StreamReader(response.ResponseStream))
                {
                    data = reader.ReadToEnd();
                }

                strData += "\n";
                strData += data;

                Debug.Log("strData:" + strData);

                if(getObjectComplete != null)
                {
                   getObjectComplete(strData);
                }
            }
            else
            {
                if(getObjectComplete != null)
                {
                   getObjectComplete("response.ResponseStream == null");
                }
            }
        });
//        return strGetObj;
    }

    /// <summary>
    /// Post Object to S3 Bucket. 
    /// </summary>
    public void PostObject(string strPostSubFolder, string strFileFolder, string strFileName, void_Result_callback postCallback = null)
    {
        string strPostResult = "Retrieving the file";
        string fileName = GetFileHelper(strFileFolder);
//        Debug.Log("fileName:"+fileName);
        var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        strPostResult += "\nCreating request object";
        var request = new PostObjectRequest()
        {
            Bucket = S3BucketName,
            Key = strPostSubFolder + strFileName,
            InputStream = stream,
            CannedACL = S3CannedACL.PublicRead,
            Region = _S3Region
        };

        strPostResult += "\nMaking HTTP post call";

//        Debug.Log(strPostResult);
        Debug.Log("[PostObject] request.Bucket=" + request.Bucket);
        Debug.Log("[PostObject] request.CannedACL=" + request.CannedACL);
        Debug.Log("[PostObject] request.Headers=" + request.Headers);
        Debug.Log("[PostObject] request.InputStream=" + request.InputStream);
        Debug.Log("[PostObject] request.Key=" + request.Key);
        Debug.Log("[PostObject] request.Metadata=" + request.Metadata);
        Debug.Log("[PostObject] request.Path=" + request.Path);
        Debug.Log("[PostObject] request.Region=" + request.Region);
        Debug.Log("[PostObject] request.SignedPolicy=" + request.SignedPolicy);
        Debug.Log("[PostObject] request.StorageClass=" + request.StorageClass);
        Debug.Log("[PostObject] request.StreamTransferProgress=" + request.StreamTransferProgress);
        Debug.Log("[PostObject] request.SuccessActionRedirect=" + request.SuccessActionRedirect);
        Debug.Log("[PostObject] request.SuccessActionStatus=" + request.SuccessActionStatus);
        Debug.Log("[PostObject] request.Headers.ContentType=" + request.Headers.ContentType);

        Client.PostObjectAsync(request, (responseObj) =>
        {
            Debug.Log("[PostObject] Client.PostObjectAsync get response");

            if (responseObj.Exception == null)
            {
                strPostResult += string.Format("\nobject {0} posted to bucket {1}", responseObj.Request.Key, responseObj.Request.Bucket);
                Debug.Log("[PostObject] strPostResult=" + strPostResult);
                Debug.Log(string.Format("[PostObject] Response.HttpStatusCode={0}", responseObj.Response.HttpStatusCode));

                if (postCallback != null)
                {
                    postCallback("Post Success:\n"+strPostResult);
                }
            }
            else
            {
                Debug.LogException(responseObj.Exception);

                strPostResult +=  "\nException while posting the result object";
//                strPostResult += string.Format("\n receieved error {0}", responseObj.Response.HttpStatusCode.ToString());                
                
                if (postCallback != null)
                {
                    postCallback("Post Fail:\n"+strPostResult);
                }
            }

//            Debug.Log(strPostResult);
        });
    }

    /// <summary>
    /// Get Objects from S3 Bucket
    /// </summary>
    public void GetObjects(void_Result_callback getObjectsCallBack = null)
    {
        string strResult = "Fetching all the Objects from " + S3BucketName;

        var request = new ListObjectsRequest()
        {
            BucketName = S3BucketName
        };

        Client.ListObjectsAsync(request, (responseObject) =>
        {
            strResult += "\n";
            if (responseObject.Exception == null)
            {
                strResult += "Got Response \n Printing now \n";
//                Debug.Log(strResult);
                responseObject.Response.S3Objects.ForEach((o) =>
                {
                    strResult += string.Format("{0}\n", o.Key);
//                    Debug.Log(strResult);
                });

                Debug.Log(strResult);
            }
            else
            {
                strResult += "Got Exception \n";
                Debug.Log(strResult);
            }

            if (getObjectsCallBack != null)
            {
                getObjectsCallBack(strResult);
            }
        });
    }

    /// <summary>
    /// DownLoad Objects from S3 Bucket
    /// </summary>
    public void DownLoadObject(string strFromSubFolder, string strToSubFileFolder, string strFileName, void_Result_callback downLoadCallback = null)
    {
        Debug.Log("DownLoadObject()");

        string strDownLoadObject = string.Format("fetching {0} from bucket {1}",
                                strFileName, S3BucketName);

        Debug.Log(strDownLoadObject);
        Client.GetObjectAsync(S3BucketName, strFromSubFolder + strFileName, (responseObj) =>
        {
            var response = responseObj.Response;
            if (response.ResponseStream != null)
            {
                Debug.Log("-------1");
                using(BinaryReader bReader = new BinaryReader(response.ResponseStream))
                {
                    byte[] buffer = bReader.ReadBytes((int)response.ResponseStream.Length);
//                    Debug.Log("1-1");
                    if (!Directory.Exists(strToSubFileFolder))
                    {
                        Directory.CreateDirectory(strToSubFileFolder);    
                    }
                    File.WriteAllBytes( strToSubFileFolder + strFileName, buffer);
//                    Debug.Log("1-2");
//                    File.WriteAllBytes( Application.persistentDataPath + Path.DirectorySeparatorChar + strFileName, buffer);
                    if (downLoadCallback != null)
                    {
                        downLoadCallback("Download Success!");
                    }
                }
//                Debug.Log("-------2");
            }
            else
            {
                if (downLoadCallback != null)
                {
                    downLoadCallback("Download Fail: response.ResponseStream == null");
                }
            }
        });
    }

    /// <summary>
    /// Delete Objects in S3 Bucket
    /// </summary>
    public void DeleteObject(string strSubFolder, string strFileName, void_Result_callback deleteCallBack = null)
    {
        Debug.Log("DeleteObject()");

        string strDeleteResult = string.Format("deleting {0} from bucket {1}", strFileName, S3BucketName);
        List<KeyVersion> objects = new List<KeyVersion>();
        objects.Add(new KeyVersion()
        {
                Key = strSubFolder + strFileName
        });

        var request = new DeleteObjectsRequest()
        {
            BucketName = S3BucketName,
            Objects = objects
        };

        Client.DeleteObjectsAsync(request, (responseObject) =>
        {
            strDeleteResult += "\n";
            if (responseObject.Exception == null)
            {
                strDeleteResult += "Got Response \n \n";
                strDeleteResult += string.Format("deleted objects \n");
                
                responseObject.Response.DeletedObjects.ForEach((dObj) =>
                {
                    strDeleteResult += dObj.Key;
                });
            }
            else
            {
                strDeleteResult += "Get Exception \n";
            }

            if (deleteCallBack != null)
            {
                deleteCallBack("strDeleteResult:"+strDeleteResult);

            }
        });
    }


    #region helper methods
    private string GetFileHelper(string strFileName)
    {
        var fileName = strFileName;
        if (!File.Exists(fileName)){
            var streamReader = File.CreateText(fileName);
            streamReader.WriteLine("This is a sample s3 file uploaded from unity s3 sample");
            streamReader.Close();
        }
            
        return fileName;
    }
        
    #endregion








	
	
}
