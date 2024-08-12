using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace VRBuilder.Editor.UserInterface.DemoScene
{
    /// <summary>
    /// Menu item for loading the demo scene after checking the process file is in the StreamingAssets folder.
    /// </summary>
    public static class DemoSceneLoader
    {
        private const int demoSceneCount = 5;
        private static readonly string[] demoScenePath = { "Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/Scenes/InfoBoxDemo.unity",
                                                           "Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/Scenes/MultipleChoiceDemo.unity",
                                                           "Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/Scenes/NumPadDemo.unity",
                                                           "Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/Scenes/UiButtonDemo.unity",
                                                           "Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/Scenes/UiOutlineDemo.unity" };

        private static readonly string[] demoProcessOrigin = { "Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/StreamingAssets/Processes/InfoBoxDemo/InfoBoxDemo.json",
                                                               "Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/StreamingAssets/Processes/MultipleChoiceDemo/MultipleChoiceDemo.json",
                                                               "Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/StreamingAssets/Processes/NumpadDemo/NumPadDemo.json",
                                                               "Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/StreamingAssets/Processes/UiButtonDemo/UiButtonDemo.json",
                                                               "Assets/MindPort/VR Builder/Add-ons/UserInterface/Demo/StreamingAssets/Processes/UiOutlineDemo/UiOutlineDemo.json" };

        private static readonly string[] demoProcessDirectory = { "Assets/StreamingAssets/Processes/InfoBoxDemo",
                                                                  "Assets/StreamingAssets/Processes/MultipleChoiceDemo",
                                                                  "Assets/StreamingAssets/Processes/NumpadDemo",
                                                                  "Assets/StreamingAssets/Processes/UiButtonDemo",
                                                                  "Assets/StreamingAssets/Processes/UiOutlineDemo" };

        private static readonly string[] demoProcessDestination = { "Assets/StreamingAssets/Processes/InfoBoxDemo/InfoBoxDemo.json",
                                                                    "Assets/StreamingAssets/Processes/MultipleChoiceDemo/MultipleChoiceDemo.json",
                                                                    "Assets/StreamingAssets/Processes/NumpadDemo/NumPadDemo.json",
                                                                    "Assets/StreamingAssets/Processes/UiButtonDemo/UiButtonDemo.json",
                                                                    "Assets/StreamingAssets/Processes/UiOutlineDemo/UiOutlineDemo.json" };

        [MenuItem("Tools/VR Builder/Demo Scenes/User Interface", false, 64)]
        public static void LoadDemoScenes()
        {

#if !VR_BUILDER_XR_INTERACTION
            if (EditorUtility.DisplayDialog("XR Interaction Component Required", "This demo scene requires VR Builder's built-in XR Interaction Component to be enabled. It looks like it is currently disabled. You can enable it in Project Settings > VR Builder > Settings.", "Ok")) 
            {
                return;
            }
#endif
            for (int i = 0; i < demoSceneCount; i++)
            {
                if (File.Exists(demoProcessDestination[i]) == false)
                {
                    Directory.CreateDirectory(demoProcessDirectory[i]);
                    FileUtil.CopyFileOrDirectory(demoProcessOrigin[i], demoProcessDestination[i]);
                }
            }

            AssetDatabase.Refresh();
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(demoScenePath[0]);
        }
    }
}