﻿using VRBuilder.Core.Conditions;
using VRBuilder.Editor.UI.StepInspector.Menu;
using VRBuilder.UI.Conditions;

namespace VRBuilder.Editor.UI.Conditions
{
    /// <inheritdoc />
    public class MultipleChoiceConditionMenuItem : MenuItem<ICondition>
    {
        /// <inheritdoc />
        public override string DisplayedName { get; } = "UI/Multiple Choice Button";

        /// <inheritdoc />
        public override ICondition GetNewItem()
        {
            return new MultipleChoiceCondition();
        }
    }
}