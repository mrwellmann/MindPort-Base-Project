
using UnityEditor;
using UnityEngine;

namespace VRBuilder.Editor.UserInterface.DemoScene
{
    public class UserInterfacePrefabsMenuItem : MonoBehaviour
    {
        [MenuItem("GameObject/VRBuilder/UI/Create Infobox", false, 10)]
        static void CreateGameObjectPreviewInfoBox(MenuCommand menuCommand)
        {
            GameObject context = (GameObject)menuCommand.context;
            CreatePreviewInfoBox(context);
        }

        static void CreatePreviewInfoBox(GameObject context)
        {
            var prefab = AssetDatabase.LoadAssetAtPath("Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/StaticAssets/Prefabs/P_InfoBox.prefab", typeof(GameObject));
            var gameObject = GameObject.Instantiate(prefab) as GameObject;
            HandleContext(gameObject, context);
            Selection.activeObject = gameObject;
        }


        [MenuItem("GameObject/VRBuilder/UI/Create Multiple Choice Box", false, 20)]
        static void CreateGameObjectMultipleChoiceBox(MenuCommand menuCommand)
        {
            GameObject context = (GameObject)menuCommand.context;
            CreateMultipleChoiceBox(context);
        }

        static void CreateMultipleChoiceBox(GameObject context)
        {
            var prefab = AssetDatabase.LoadAssetAtPath("Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/StaticAssets/Prefabs/P_MultipleChoice.prefab", typeof(GameObject));
            var gameObject = GameObject.Instantiate(prefab) as GameObject;
            HandleContext(gameObject, context);
            Selection.activeObject = gameObject;
        }

        [MenuItem("GameObject/VRBuilder/UI/Create Num Pad", false, 30)]
        static void CreateGameObjectNumpad(MenuCommand menuCommand)
        {
            GameObject context = (GameObject)menuCommand.context;
            CreateNumpad(context);
        }

        static void CreateNumpad(GameObject context)
        {
            var prefab = AssetDatabase.LoadAssetAtPath("Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/StaticAssets/Prefabs/P_NumPad.prefab", typeof(GameObject));
            var gameObject = GameObject.Instantiate(prefab) as GameObject;
            HandleContext(gameObject, context);
            Selection.activeObject = gameObject;
        }


        static void HandleContext(GameObject gameObject, GameObject context)
        {
            if (context != null)
            {
                gameObject.transform.position = context.transform.position;
                gameObject.transform.rotation = context.transform.rotation;
            }
            else
            {
                gameObject.transform.position = Vector3.zero;
                gameObject.transform.rotation = Quaternion.identity;
            }
        }
    }
}
