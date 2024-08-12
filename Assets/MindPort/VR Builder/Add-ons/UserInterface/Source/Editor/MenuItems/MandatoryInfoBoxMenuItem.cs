using VRBuilder.Core.Conditions;
using VRBuilder.Editor.UI.StepInspector.Menu;
using VRBuilder.UI.Conditions;

namespace VRBuilder.Editor.UI.Conditions
{
    /// <inheritdoc />
    public class MandatoryInfoBoxMenuItem : MenuItem<ICondition>
    {
        /// <inheritdoc />
        public override string DisplayedName { get; } = "UI/Mandatory Infobox";

        /// <inheritdoc />
        public override ICondition GetNewItem()
        {
            return new MandatoryInfoBoxCondition();
        }
    }
}
