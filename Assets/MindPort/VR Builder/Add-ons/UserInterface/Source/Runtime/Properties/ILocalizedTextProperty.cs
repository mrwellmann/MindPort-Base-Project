using VRBuilder.Core.Properties;

namespace VRBuilder.UI.Properties
{
    /// <summary>
    /// Interface for Set Localized Text Behavior
    /// </summary>
    public interface ILocalizedTextProperty : ISceneObjectProperty
    {
        void AppendText(string localizationTable, string text);
        void SetText(string localizationTable, string text);
    }
}
