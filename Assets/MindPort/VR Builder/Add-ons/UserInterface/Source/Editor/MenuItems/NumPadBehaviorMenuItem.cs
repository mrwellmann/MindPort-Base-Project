using VRBuilder.Core.Behaviors;
using VRBuilder.Editor.UI.StepInspector.Menu;
using VRBuilder.UI.Behaviors;

namespace VRBuilder.Editor.UI.Behaviors
{
    /// <inheritdoc />
    public class NumPadBehaviorMenuItem : MenuItem<IBehavior>
    {
        /// <inheritdoc />
        public override string DisplayedName { get; } = "UI/Num Pad";

        /// <inheritdoc />
        public override IBehavior GetNewItem()
        {
            return new NumPadBehavior();
        }
    }
}
