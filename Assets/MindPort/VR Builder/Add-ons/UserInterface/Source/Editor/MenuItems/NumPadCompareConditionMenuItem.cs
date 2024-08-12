using VRBuilder.Core.Conditions;
using VRBuilder.Editor.UI.StepInspector.Menu;
using VRBuilder.UI.Conditions;

namespace VRBuilder.Editor.UI.Conditions
{
    /// <inheritdoc />
    public class NumPadCompareConditionMenuItem : MenuItem<ICondition>
    {
        /// <inheritdoc />
        public override string DisplayedName { get; } = "States and Data/Compare Num Pad Numbers";

        /// <inheritdoc />
        public override ICondition GetNewItem()
        {
            return new NumPadCompareCondition();
        }
    }
}