using UnityEngine;
using System.Collections;

public class AssetBundleLoader : MonoBehaviour
{
    public bool DrawTestingButton = false;
    public string FilePath = "";
    public event System.EventHandler BuildCompleteEvent;
    
    private WWW _wwwObj = null;
    private GameObject _buildedObject = null;

    IEnumerator Build()
    {
        if (_wwwObj != null)
        {
            _wwwObj.Dispose();
            _wwwObj = null;
        }

        _wwwObj = new WWW(FilePath);

        while (!_wwwObj.isDone)
        {
            Debug.Log("building progress: " + (int)(_wwwObj.progress * 100f) + "%");
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        Debug.Log("building progress: " + 100f + "%");

        if (string.IsNullOrEmpty(_wwwObj.error))
        {
            AssetBundle ab = _wwwObj.assetBundle;
            Object mainAsset = ab.mainAsset;

            _buildedObject = GameObject.Instantiate(mainAsset) as GameObject;
            if (BuildCompleteEvent != null)
            {
                BuildCompleteEvent(_buildedObject, null);
            }
        }
        else
        {
            Debug.LogError(_wwwObj.error);
        }
    }

    // test
    void OnGUI()
    {
        if (!DrawTestingButton)
            return;
        
        FilePath = GUI.TextField(new Rect(0, 0, 1024, 100), FilePath);

        if (GUI.Button(new Rect(0, 100, 100, 100), "build"))
        {
            this.StopAllCoroutines();
            this.StartCoroutine(this.Build());
        }
    }
}
