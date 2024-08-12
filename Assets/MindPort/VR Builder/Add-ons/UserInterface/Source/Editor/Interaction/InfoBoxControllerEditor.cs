using UnityEditor;
using UnityEngine;
using VRBuilder.UI.Interaction;

namespace VRBuilder.Editor.UI.Interaction
{
    /// <summary>
    /// Custom Editor for <see cref="InfoBoxController"/>.
    /// </summary>    
    [CustomEditor(typeof(InfoBoxController))]
    public class InfoBoxControllerEditor : UnityEditor.Editor
    {
        protected bool _showAdvancedSettings = false;
        protected bool _showTitle = false;
        protected bool _showDescription = false;
        protected bool _showImpact = false;
        protected bool _showImage = false;
        protected float _infoBoxSize = -1f;

        private static class Tooltips
        {
            public static readonly GUIContent FoldoutAdvancedSettings = new GUIContent("Advanced Settings", "");
            public static readonly GUIContent FoldoutTitle = new GUIContent("Title Text", "");
            public static readonly GUIContent FoldoutDescription = new GUIContent("Detailed Text", "");
            public static readonly GUIContent FoldoutImage = new GUIContent("Image", "");

            public static readonly GUIContent Mode = new GUIContent("Display Mode", "");
            public static readonly GUIContent Type = new GUIContent("Layout Type", "");
            public static readonly GUIContent TitleSprite = new GUIContent("Title Sprite", "");
            public static readonly GUIContent TitlePreview = new GUIContent("Edit Title Text", "");
            public static readonly GUIContent Title = new GUIContent("Saved Title Text", "");
            public static readonly GUIContent DescriptionPreview = new GUIContent("Edit Detailed Text", "");
            public static readonly GUIContent Description = new GUIContent("Saved Detailed Text", "");
            public static readonly GUIContent DescriptionTitleTextPreview = new GUIContent("Details Title Text Preview", "");
            public static readonly GUIContent ImageSprite = new GUIContent("Image Sprite", "");
            public static readonly GUIContent ImageColor = new GUIContent("Image Color", "");
            public static readonly GUIContent AcknowledgeButtonTextPreview = new GUIContent("Acknowledge Button Text Preview", "");
            public static readonly GUIContent IStandAlone = new GUIContent("Standalone Infobox", "");
            public static readonly GUIContent StartOptionalExpanded = new GUIContent("Start Optional Infobox Expanded", "");

            public static readonly GUIContent LocalizationTable = new GUIContent("Use Localization Table", "");
            public static readonly GUIContent TitleKey = new GUIContent("Title Key", "");
            public static readonly GUIContent DescriptionKey = new GUIContent("Description Key", "");
            public static readonly GUIContent DescriptionTitleKey = new GUIContent("Description Header Key", "");
        }

        public override void OnInspectorGUI()
        {
            InfoBoxController infoBox = (InfoBoxController)target;

            serializedObject.Update();

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField(EditorGUIUtility.TrTempContent("Script"), MonoScript.FromMonoBehaviour((InfoBoxController)target), typeof(InfoBoxController), false);
            EditorGUI.EndDisabledGroup();

            EditorStyles.textField.wordWrap = true;

            var type = serializedObject.FindProperty("Type");
            EditorGUILayout.PropertyField(type, Tooltips.Type);

            bool useLoca = !string.IsNullOrEmpty(infoBox.LocalizationTable);

            EditorGUILayout.BeginHorizontal();

            var sprite = serializedObject.FindProperty("TitleSprite");
            EditorGUILayout.PropertyField(sprite, Tooltips.TitleSprite);

            EditorGUILayout.EndHorizontal();

            var infoBoxSize = serializedObject.FindProperty("InfoBoxSize");
            EditorGUILayout.Slider(infoBoxSize, 0.1f, 10f);

            var localizationTable = serializedObject.FindProperty("LocalizationTable");
            EditorGUILayout.PropertyField(localizationTable, Tooltips.LocalizationTable);

            if (useLoca)
            {
                var titleKey = serializedObject.FindProperty("Title");
                EditorGUILayout.PropertyField(titleKey, Tooltips.TitleKey);

                var descriptionKey = serializedObject.FindProperty("Description");
                EditorGUILayout.PropertyField(descriptionKey, Tooltips.DescriptionKey);

                var descriptionTitleKey = serializedObject.FindProperty("DescriptionTitle");
                EditorGUILayout.PropertyField(descriptionTitleKey, Tooltips.DescriptionTitleKey);
            }
            else
            {
                _showTitle = EditorGUILayout.BeginFoldoutHeaderGroup(_showTitle, Tooltips.FoldoutTitle, EditorStyles.foldoutHeader);
                if (_showTitle)
                {
                    EditorGUILayout.LabelField(Tooltips.TitlePreview);
                    infoBox.Title = EditorGUILayout.TextArea(infoBox.Title.Replace("<br>", "\n"));
                    var title = serializedObject.FindProperty("Title");
                    title.stringValue = infoBox.Title.Replace("\n", "<br>");

                    EditorGUILayout.Space();
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(title, Tooltips.Title);
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUILayout.EndFoldoutHeaderGroup();

                _showDescription = EditorGUILayout.BeginFoldoutHeaderGroup(_showDescription, Tooltips.FoldoutDescription, EditorStyles.foldoutHeader);
                if (_showDescription)
                {
                    EditorGUILayout.LabelField(Tooltips.DescriptionPreview);
                    infoBox.Description = EditorGUILayout.TextArea(infoBox.Description.Replace("<br>", "\n"), GUILayout.Height(80));
                    var description = serializedObject.FindProperty("Description");
                    description.stringValue = infoBox.Description.Replace("\n", "<br>");

                    EditorGUILayout.Space();
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(description, Tooltips.Description);
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }

            _showImage = EditorGUILayout.BeginFoldoutHeaderGroup(_showImage, Tooltips.FoldoutImage, EditorStyles.foldoutHeader);
            if (_showImage)
            {
                var imageSprite = serializedObject.FindProperty("ImageSprite");
                EditorGUILayout.PropertyField(imageSprite, Tooltips.ImageSprite);

                var imageColor = serializedObject.FindProperty("ImageColor");
                EditorGUILayout.PropertyField(imageColor, Tooltips.ImageColor);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            _showAdvancedSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showAdvancedSettings, Tooltips.FoldoutAdvancedSettings, EditorStyles.foldoutHeader);
            if (_showAdvancedSettings)
            {
                var infoBoxPanel = serializedObject.FindProperty("PreviewInfoBoxCanvas");
                EditorGUILayout.PropertyField(infoBoxPanel);

                var titleText = serializedObject.FindProperty("TitleText");
                EditorGUILayout.PropertyField(titleText);

                var titleIcon = serializedObject.FindProperty("TitleIcon");
                EditorGUILayout.PropertyField(titleIcon);

                var exitButtonTransform = serializedObject.FindProperty("ExitButtonTransform");
                EditorGUILayout.PropertyField(exitButtonTransform);

                var detailsContainerTransform = serializedObject.FindProperty("DetailsContainerTransform");
                EditorGUILayout.PropertyField(detailsContainerTransform);

                var detailsTitleText = serializedObject.FindProperty("DetailsTitleText");
                EditorGUILayout.PropertyField(detailsTitleText);

                var detailsDescriptionText = serializedObject.FindProperty("DetailsDescriptionText");
                EditorGUILayout.PropertyField(detailsDescriptionText);

                if (infoBox.DetailsTitleText != null)
                {
                    EditorGUILayout.LabelField(Tooltips.DescriptionTitleTextPreview);
                    infoBox.DetailsTitleText.text = EditorGUILayout.TextField(infoBox.DetailsTitleText.text);
                }

                var ScaleDetailsContainerWithTextSize = serializedObject.FindProperty("ScaleDetailsContainerWithTextSize");
                EditorGUILayout.PropertyField(ScaleDetailsContainerWithTextSize);

                var scaleDetailsContainerTextSizeFactor = serializedObject.FindProperty("ScaleDetailsContainerTextSizeFactor");
                EditorGUILayout.PropertyField(scaleDetailsContainerTextSizeFactor);

                var imageContainerTransform = serializedObject.FindProperty("ImageContainerTransform");
                EditorGUILayout.PropertyField(imageContainerTransform);

                var imageIcon = serializedObject.FindProperty("ImageIcon");
                EditorGUILayout.PropertyField(imageIcon);

                var acknowledgeButtonTransform = serializedObject.FindProperty("AcknowledgeButtonTransform");
                EditorGUILayout.PropertyField(acknowledgeButtonTransform);

                var acknowledgeButtonText = serializedObject.FindProperty("AcknowledgeButtonText");
                EditorGUILayout.PropertyField(acknowledgeButtonText);

                if (infoBox.AcknowledgeButtonText != null)
                {
                    EditorGUILayout.LabelField(Tooltips.AcknowledgeButtonTextPreview);
                    infoBox.AcknowledgeButtonText.text = EditorGUILayout.TextArea(infoBox.AcknowledgeButtonText.text);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            var changed = serializedObject.ApplyModifiedProperties();

            //Preview!
            infoBox.SetInfoBoxType(infoBox.Type);
            infoBox.SetTexts(infoBox.LocalizationTable, infoBox.Title, infoBox.Description);
            infoBox.SetTitleImage(infoBox.TitleSprite);
            infoBox.SetImage(infoBox.ImageSprite, infoBox.ImageColor);
            if (infoBox.InfoBoxSize != _infoBoxSize)
            {
                infoBox.SetInfoBoxSize(infoBox.InfoBoxSize);
                _infoBoxSize = infoBox.InfoBoxSize;
            }
            if (changed) EditorUtility.SetDirty(infoBox);
        }
    }

    public static class Texture2DExtensions
    {
        public static void SetColor(this Texture2D tex2, Color32 color)
        {
            var fillColorArray = tex2.GetPixels32();

            for (var i = 0; i < fillColorArray.Length; ++i)
            {
                fillColorArray[i] = color;
            }

            tex2.SetPixels32(fillColorArray);

            tex2.Apply();
        }
    }
}



