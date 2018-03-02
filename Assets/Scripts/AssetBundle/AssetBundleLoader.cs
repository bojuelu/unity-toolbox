using UnityEngine;
using System.Collections;

namespace UnityToolbox
{
    public class AssetBundleLoader : MonoBehaviour
    {
        public bool drawTestingButton = false;
        public string filePath = "";
        public event System.EventHandler buildCompleteEvent;

        private WWW wwwObj = null;
        private GameObject buildedObject = null;

        IEnumerator Build()
        {
            if (wwwObj != null)
            {
                wwwObj.Dispose();
                wwwObj = null;
            }

            wwwObj = new WWW(filePath);

            while (!wwwObj.isDone)
            {
                Debug.Log("building progress: " + (int)(wwwObj.progress * 100f) + "%");
                yield return null;
            }
            yield return new WaitForEndOfFrame();
            Debug.Log("building progress: " + 100f + "%");

            if (string.IsNullOrEmpty(wwwObj.error))
            {
                AssetBundle ab = wwwObj.assetBundle;
                Object mainAsset = ab.mainAsset;

                buildedObject = GameObject.Instantiate(mainAsset) as GameObject;
                if (buildCompleteEvent != null)
                {
                    buildCompleteEvent(buildedObject, null);
                }
            }
            else
            {
                Debug.LogError(wwwObj.error);
            }
        }

        // test
        void OnGUI()
        {
            if (!drawTestingButton)
                return;

            filePath = GUI.TextField(new Rect(0, 0, 1024, 100), filePath);

            if (GUI.Button(new Rect(0, 100, 100, 100), "build"))
            {
                this.StopAllCoroutines();
                this.StartCoroutine(this.Build());
            }
        }
    }
}