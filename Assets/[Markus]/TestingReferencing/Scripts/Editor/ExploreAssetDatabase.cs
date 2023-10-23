using System.IO;
using UnityEditor;
using UnityEngine;

public class ExploreAssetDatabase : MonoBehaviour
{
    [MenuItem("Tools/Custom/Explore Asset Database")]
    public static void Explore()
    {
        string[] allAssetGUIDs = AssetDatabase.FindAssets("t:Object");
        foreach (string guid in allAssetGUIDs)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Object asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            FileInfo fileInfo = new FileInfo(assetPath);
            if (fileInfo.Exists)
            {
                Debug.Log("Asset: " + asset.name + " (" + asset.GetType() + ") at path: " + assetPath + ", modification time: " + fileInfo.LastWriteTime);
            }
        }
    }
}
