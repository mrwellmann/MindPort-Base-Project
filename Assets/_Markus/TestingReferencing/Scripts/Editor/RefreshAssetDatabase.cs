using UnityEditor;
using UnityEngine;

public class RefreshAssetDatabase : MonoBehaviour
{
    [MenuItem("Tools/Custom/Refresh Asset Database")]
    public static void Refresh()
    {
        AssetDatabase.Refresh();
    }
}