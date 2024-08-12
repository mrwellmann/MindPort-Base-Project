using VRBuilder.Core.Behaviors;
using VRBuilder.Editor.UI.StepInspector.Menu;
using VRBuilder.UI.Behaviors;

namespace VRBuilder.Editor.UI.Behaviors
{
    public class UiOutlineMenuItem : MenuItem<IBehavior>
    {
        /// <inheritdoc />
        public override string DisplayedName { get; } = "UI/Outline";

        /// <inheritdoc />
        public override IBehavior GetNewItem()
        {
            return new UiOutlineBehavior();
        }
    }
}