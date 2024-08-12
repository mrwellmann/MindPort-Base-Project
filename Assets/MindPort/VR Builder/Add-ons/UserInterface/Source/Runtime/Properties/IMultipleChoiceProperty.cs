using VRBuilder.Core.Properties;

namespace VRBuilder.UI.Properties
{
    /// <summary>
    /// Interface for Multiple Choice Box Behavior and Conditions
    /// </summary>
    public interface IMultipleChoiceProperty : ISceneObjectProperty
    {
        bool IsValid { get; }
        void SetMultipleChoiceBoxTitle(string text);
        void SetMultipleChoiceBoxTitle(string localizationTable, string text);
        void SetVerticalButtonLayout(bool enabled);
        void CreateMultipleChoiceButton(string text, bool isSprite);
        void CreateMultipleChoiceButton(string localizationTable, string text, bool isSprite);
        void ShowMultipleChoiceBox(float delay = 0f);
        void HideMultipleChoiceBox();
        void ResetMultipleChoiceBox();
        bool IsButtonPressed(string text);
        bool IsButtonPressed(string localizationTable, string text, bool isSprite);
        void SetHideDelay(int seconds);
    }
}
