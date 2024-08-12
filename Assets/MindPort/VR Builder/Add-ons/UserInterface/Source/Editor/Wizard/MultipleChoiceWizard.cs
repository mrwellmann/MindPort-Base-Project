using UnityEditor;
using UnityEngine;
using VRBuilder.Editor;
using VRBuilder.Core.Behaviors;
using VRBuilder.Core.Conditions;
using System.Collections.Generic;
using VRBuilder.Core;
using VRBuilder.Editor.UI.Graphics;
using VRBuilder.Editor.UI.Windows;
using VRBuilder.UI.Behaviors;
using VRBuilder.UI.Conditions;
using VRBuilder.UI.Properties;
using VRBuilder.UI.Utils;
using System.Linq;

namespace VRBuilder.Editor.UI.Wizard
{

    public class MultipleChoiceWizard : EditorWindow
    {
        protected string question = "Question";
        protected int numberOfAnswers = 2;
        protected string[] answers = new string[2];
        protected bool verticalLayout = false;

        // Add menu named "My Window" to the Window menu
        [MenuItem("Tools/VR Builder/MultipleChoiceWizard")]
        [MenuItem("GameObject/VRBuilder/UI/Multiple Choice Wizard", false, 40)]
        static void Init()
        {
            // Get existing open window or if none, make a new one:
            MultipleChoiceWizard window = (MultipleChoiceWizard)EditorWindow.GetWindow(typeof(MultipleChoiceWizard));
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("Create Multiple Choice Box Behavior and Conditions", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            question = EditorGUILayout.TextField("Question", question);
            numberOfAnswers = EditorGUILayout.IntField("Number of Answers", numberOfAnswers);
            numberOfAnswers = Mathf.Clamp(numberOfAnswers, 2, 20);
            if (answers.Length != numberOfAnswers) answers = new string[numberOfAnswers];

            EditorGUILayout.Space();

            for (int i = 0; i < numberOfAnswers; i++)
            {
                answers[i] = EditorGUILayout.TextField("Answer " + (i + 1), answers[i]);
            }

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Vertical Layout ? ", EditorStyles.boldLabel);
            verticalLayout = EditorGUILayout.Toggle(verticalLayout);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Create Step"))
            {
                CreateStep();
            }
        }

        void CreateStep()
        {
            var step = VRBuilderEditorHelper.CreateStep("Multiple Choice Step");

            var mcBehavior = new MultipleChoiceBehavior();
            mcBehavior.Data.Title = question;
            mcBehavior.Data.VerticalButtonLayout = verticalLayout;
            VRBuilderEditorHelper.AddBehaviorToStep(step, mcBehavior);

            VRBuilderEditorHelper.ClearTransationsOfStep(step);
            for (int i = 0; i < numberOfAnswers; i++)
            {
                var transition = VRBuilderEditorHelper.AddTransationToStep(step);
                var mcCondition = new MultipleChoiceCondition();
                mcCondition.Data.ButtonText = answers[i];
                transition.Data.Conditions.Add(mcCondition);
            }

            //VRBuilderEditorHelper.RefreshProcessWindow();
            VRBuilderEditorHelper.RefreshProcessGraphViewWindowWindow();
        }


        protected static IMultipleChoiceProperty FindValidMultipleChoiceBox()
        {
            return InterfaceExtensions.FindInterfaceOfType<IMultipleChoiceProperty>().FirstOrDefault(mcBox => mcBox.IsValid);
        }

    }
}
