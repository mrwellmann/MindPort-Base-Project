using VRBuilder.Core.Properties;

namespace VRBuilder.UI.Properties
{
    public enum InfoBoxType
    {
        Optional,
        Mandatory,
        InfoPanel
    }

    /// <summary>
    /// Interface for Optional InfoBox Behavior and Mandatory Infobox Condition 
    /// </summary>
    public interface IInfoBoxProperty : ISceneObjectProperty
    {
        void SetInfoBoxType(InfoBoxType type);
        void ShowInfoBox(bool show, float delay = 0f);
        bool IsCompleted { get; }
        bool IsValid { get; }
        InfoBoxType InfoBoxType { get; }
    }
}
