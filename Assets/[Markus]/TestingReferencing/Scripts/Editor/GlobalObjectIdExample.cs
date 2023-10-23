using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GlobalObjectIdExample
{
    [MenuItem("Tools/Custom/GlobalObjectId")]
    static void MenuCallback()
    {
        const string testScenePath = "Assets/MyTestScene.unity";

        var stringIds = CreateSceneWithTwoObjects(testScenePath);

        // These string formatted ids could be saved to a file, then retrieved in a later session of Unity
        Debug.Log("Ids of new objects " + stringIds[0] + " and " + stringIds[1]);

        ReloadSceneAndResolveObjects(testScenePath, stringIds);
    }

    [MenuItem("Tools/Custom/GlobalObjectIdPrefab")]
    static void MenuCallback2()
    {
        const string testScenePath = "Assets/MyTestScene.unity";

        var stringIds = CreateSceneWithTwoObjects(testScenePath);

        // These string formatted ids could be saved to a file, then retrieved in a later session of Unity
        Debug.Log("Ids of new objects " + stringIds[0] + " and " + stringIds[1]);

        ReloadSceneAndResolveObjects(testScenePath, stringIds);
    }

    static string[] CreateSceneWithTwoObjects(string testScenePath)
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // Scene must have been serialized at least once prior to generating GlobalObjectIds, so that the asset guid is available
        EditorSceneManager.SaveScene(scene, testScenePath);

        var objects = new Object[2];
        objects[0] = GameObject.CreatePrimitive(PrimitiveType.Plane);
        objects[0].name = "MyPlane";
        objects[1] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        objects[1].name = "MyCube";

        var ids = new GlobalObjectId[2];
        GlobalObjectId.GetGlobalObjectIdsSlow(objects, ids);

        EditorSceneManager.SaveScene(scene, testScenePath);

        // Close the scene
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        var idsStringFormat = new string[2];
        idsStringFormat[0] = ids[0].ToString();
        idsStringFormat[1] = ids[1].ToString();

        return idsStringFormat;
    }

    static void ReloadSceneAndResolveObjects(string testScenePath, string[] objectIdsAsStrings)
    {
        var ids = new GlobalObjectId[2];
        GlobalObjectId.TryParse(objectIdsAsStrings[0], out ids[0]);
        GlobalObjectId.TryParse(objectIdsAsStrings[1], out ids[1]);

        // The scene must be loaded before the ids to objects it contains can be resolved
        EditorSceneManager.OpenScene(testScenePath);

        var objects = new Object[2];
        GlobalObjectId.GlobalObjectIdentifiersToObjectsSlow(ids, objects);

        // Found MyPlane and MyCube
        Debug.Log("Found " + objects[0].name + " and " + objects[1].name);
    }
}