using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleBuilder
{
    const string kMenu = "Assets/Build AssetBundles/";

    [MenuItem(kMenu + "Android")]
    static void CreateAssetBunldesForAndroid()
    {
        ExecCreateAssetBunldes(BuildTarget.Android);
    }

    [MenuItem(kMenu + "iOS")]
    static void CreateAssetBunldesForIOS()
    {
        ExecCreateAssetBunldes(BuildTarget.iOS);
    }

    static void ExecCreateAssetBunldes(BuildTarget buildTarget)
    {
        if (EditorApplication.isPlaying || EditorApplication.isPaused)
        {
            Debug.LogWarning("Stop the editor first");
            return;
        }

        string targetDir = "AssetBundles/" + buildTarget.ToString();
        string extensionName = ".asset";

        if (!Directory.Exists(targetDir))
            Directory.CreateDirectory(targetDir);
        
        Object[] selectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);

        Debug.Log("Select object count: " + selectedAsset.Length);
        Debug.Log("Object list:");
        for (int i = 0; i < selectedAsset.Length; i++)
        {
            Object obj = selectedAsset[i];
            Debug.Log(obj.name);
        }

        for (int i = 0; i < selectedAsset.Length; i++)
        {
            Object obj = selectedAsset[i];

            //string sourcePath = AssetDatabase.GetAssetPath(obj);
            string targetPath = targetDir + Path.DirectorySeparatorChar + obj.name + extensionName;

            if (File.Exists(targetPath))
                File.Delete(targetPath);

            if (!(obj is GameObject) && !(obj is Texture2D) && !(obj is Material))
            {
                Debug.LogWarning(obj.name + " can not build for assetbundle, skip it.");
                continue;
            }

            if (BuildPipeline.BuildAssetBundle(obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies, buildTarget))
            {
                Debug.Log(targetPath + " build complete");
            }
            else
            {  
                Debug.Log(targetPath + " build failed");
            }
        }
    }

}
