using VRBuilder.Core.Conditions;
using VRBuilder.Editor.UI.StepInspector.Menu;
using VRBuilder.UI.Conditions;

namespace VRBuilder.Editor.UI.Conditions
{
    public class UiButtonMenuItem : MenuItem<ICondition>
    {
        public override string DisplayedName { get; } = "UI/Button Click";

        public override ICondition GetNewItem()
        {
            return new UiButtonCondition();
        }
    }
}