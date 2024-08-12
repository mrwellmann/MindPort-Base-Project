using VRBuilder.Core;
using VRBuilder.Core.Behaviors;
using VRBuilder.Core.Conditions;
using System.Collections.Generic;
using UnityEditor;
using VRBuilder.Editor.UI.Windows;
using VRBuilder.Editor.UI.Graphics;

namespace VRBuilder.Editor
{
    public static class VRBuilderEditorHelper
    {
        public static IStep CreateStep(string stepName)
        {
            var chapter = GlobalEditorHandler.GetCurrentChapter();

            IStep step = EntityFactory.CreateStep(stepName);
            chapter.Data.Steps.Add(step);
            chapter.ChapterMetadata.LastSelectedStep = step;
            return step;
        }

        public static IStep GetLastSelectedStep()
        {
            var chapter = GlobalEditorHandler.GetCurrentChapter();
            return chapter.ChapterMetadata.LastSelectedStep;
        }

        public static void ClearTransationsOfStep(IStep step)
        {
            ITransition transition = new Transition();
            var transitions = step.Data.Transitions;
            if (transitions != null)
            {
                transitions.Data.Transitions.Clear();
            }
        }

        public static ITransition AddTransationToStep(IStep step)
        {
            ITransition transition = new Transition();
            var transitions = step.Data.Transitions;
            if (transitions != null)
            {
                transitions.Data.Transitions.Add(transition);
            }

            return transition;
        }

        public static void AddBehaviorToStep(IStep step, IBehavior item)
        {
            var behaviors = step.Data.Behaviors;
            if (behaviors != null)
            {
                behaviors.Data.Behaviors.Add(item);
            }
        }

        public static IList<IBehavior> GetStepBehaviors(IStep step)
        {
            var behaviors = step.Data.Behaviors;
            if (behaviors != null)
            {
                return behaviors.Data.Behaviors;
            }
            return null;
        }

        public static void AddConditionToTransition(ITransition transition, ICondition item)
        {
            var conditions = transition.Data.Conditions;
            if (conditions != null)
            {
                conditions.Add(item);
            }
        }

        public static void RefreshProcessWindow()
        {
            ProcessWindow window = (ProcessWindow)EditorWindow.GetWindow(typeof(ProcessWindow));
            window.Focus();
            window.RefreshChapterRepresentation();
        }

        public static void RefreshProcessGraphViewWindowWindow()
        {
            ProcessGraphViewWindow window = (ProcessGraphViewWindow)EditorWindow.GetWindow(typeof(ProcessGraphViewWindow));
            window.Focus();
            window.SetChapter(window.GetChapter());
            window.RefreshChapterRepresentation();
        }

        public static IEnumerable<IStep> FindStepsWithCondition(ICondition item)
        {
            var chapter = GlobalEditorHandler.GetCurrentChapter();

            foreach (var step in chapter.Data.Steps)
            foreach (var transition in step.Data.Transitions.Data.Transitions)
            foreach (var condition in transition.Data.Conditions)
                if (condition.GetType().DeclaringType == item.GetType().DeclaringType)
                    yield return step;
        }

        public static List<Step> GetStepList(IChapter chapter)
        {
            List<Step> steps = new List<Step>();
            foreach (var iStep in chapter.Data.Steps)
            {
                var step = iStep as Step;
                if (step != null)
                {
                    steps.Add(step);
                }
            }
            return steps;
        }

        public static List<Chapter> GetChapterList()
        {
            List<Chapter> chapters = new List<Chapter>();

            var process = GlobalEditorHandler.GetCurrentProcess();
            if (process == null)
            {
                process = ProcessRunner.Current;
            }

            if (process != null)
            {
                IList<IChapter> iChapters = process.Data.Chapters;
                foreach (var iChapter in iChapters)
                {
                    var chapter = iChapter as Chapter;
                    if (chapter != null)
                    {
                        chapters.Add(chapter);
                    }
                }
            }
            return chapters;
        }
    }
}