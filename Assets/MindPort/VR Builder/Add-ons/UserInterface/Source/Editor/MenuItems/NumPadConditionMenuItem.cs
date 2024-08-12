using VRBuilder.Core.Conditions;
using VRBuilder.Editor.UI.StepInspector.Menu;
using VRBuilder.UI.Conditions;

namespace VRBuilder.Editor.UI.Conditions
{
    /// <inheritdoc />
    public class NumPadConditionMenuItem : MenuItem<ICondition>
    {
        /// <inheritdoc />
        public override string DisplayedName { get; } = "UI/Num Pad Enter Button";

        /// <inheritdoc />
        public override ICondition GetNewItem()
        {
            return new NumPadCondition();
        }
    }
}
