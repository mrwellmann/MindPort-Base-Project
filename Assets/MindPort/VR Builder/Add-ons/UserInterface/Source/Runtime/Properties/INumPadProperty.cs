using VRBuilder.Core.Properties;

namespace VRBuilder.UI.Properties
{
    public interface INumPadProperty: ISceneObjectProperty
    {
        bool IsDataAccepted();
        bool IsValid { get; }
        void SetNumPadVisibility(bool visible);
        void ResetNumPad(bool resetEnteredValue);
        void InitNumPad();
        int GetValue();
    }
}