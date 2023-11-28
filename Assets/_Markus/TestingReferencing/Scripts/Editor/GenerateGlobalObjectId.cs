using UnityEditor;
using UnityEngine;

public class GenerateGlobalObjectIdWindow : EditorWindow
{
    private Object testObject;

    [MenuItem("Tools/Custom/GenerateGlobalObjectId")]
    public static void ShowWindow()
    {
        GetWindow<GenerateGlobalObjectIdWindow>("Generate Global ObjectId");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Select an Object to generate a GlobalObjectId", EditorStyles.boldLabel);

        EditorGUI.BeginChangeCheck();
        testObject = EditorGUILayout.ObjectField("Unity Object", testObject, typeof(Object), true);
        EditorGUILayout.Space();
        if (EditorGUI.EndChangeCheck() && testObject != null)
        {
            string globalObjectId = CreateGlobalObjectId(testObject);
            Debug.Log($"The globalObjectId of {testObject.name} is {globalObjectId}");
        }
    }


    private string CreateGlobalObjectId(Object testObject)
    {
        var id = GlobalObjectId.GetGlobalObjectIdSlow(testObject);
        return id.ToString();
    }
}
