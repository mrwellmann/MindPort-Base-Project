using System;
using UnityEngine;
using VRBuilder.Core.ProcessUtils;
using VRBuilder.Core.SceneObjects;
using VRBuilder.UI.Conditions;
using VRBuilder.UI.Properties;
using VRBuilder.Editor.UI;
using VRBuilder.Editor.Core.UI.Drawers;
using VRBuilder.Core.Conditions;
using VRBuilder.Core.Properties;

namespace VRBuilder.Editor.UI.Drawers
{
    /// <summary>
    /// Custom drawer for <see cref="NumPadCompareCondition"/>.
    /// </summary>   
    [DefaultProcessDrawer(typeof(NumPadCompareCondition.EntityData))]
    public class NumPadCompareDrawer : NameableDrawer
    {
        /// <summary>
        /// Draws the dropdown for selecting the operator depending on the operands' type
        /// </summary>
        private enum Operator
        {
            EqualTo,
            NotEqualTo,
            GreaterThan,
            LessThan,
            GreaterThanOrEqual,
            LessThanOrEqual,
        }

        public override Rect Draw(Rect rect, object currentValue, Action<object> changeValueCallback, GUIContent label)
        {
            rect = base.Draw(rect, currentValue, changeValueCallback, label);

            float height = DrawLabel(rect, currentValue, changeValueCallback, label);

            height += EditorDrawingHelper.VerticalSpacing;

            Rect nextPosition = new Rect(rect.x, rect.y + height, rect.width, rect.height);

            NumPadCompareCondition.EntityData data = currentValue as NumPadCompareCondition.EntityData;

            SingleScenePropertyReference<INumPadProperty> left = data.LeftTarget;
            nextPosition = DrawerLocator.GetDrawerForValue(left, typeof(SingleScenePropertyReference<INumPadProperty>)).Draw(nextPosition, left, changeValueCallback, "Num Pad");

            height += nextPosition.height;
            height += EditorDrawingHelper.VerticalSpacing;
            nextPosition.y = rect.y + height;

            Operator currentOperator = GetCurrentOperator(data);
            nextPosition = DrawerLocator.GetDrawerForValue(currentOperator, typeof(Operator)).Draw(nextPosition, currentOperator, (value) => UpdateOperator(value, data, changeValueCallback), "Operator"); height += nextPosition.height;
            height += EditorDrawingHelper.VerticalSpacing;
            nextPosition.y = rect.y + height;

            nextPosition = DrawerLocator.GetDrawerForValue(data.CompareValue, data.CompareValue.GetType()).Draw(nextPosition, data.CompareValue, (value) => UpdateState(value, data, changeValueCallback), "State");
            height += nextPosition.height;
            height += EditorDrawingHelper.VerticalSpacing;
            nextPosition.y = rect.y + height;

            rect.height = height;
            return rect;
        }

        private void UpdateState(object value, NumPadCompareCondition.EntityData data, Action<object> changeValueCallback)
        {
            int oldValue = data.CompareValue;
            int newValue = (int)value;

            if (newValue != oldValue)
            {
                data.CompareValue = newValue;
                changeValueCallback(data);
            }
        }

        private void UpdateOperator(object value, NumPadCompareCondition.EntityData data, Action<object> changeValueCallback)
        {
            Operator newOperator = (Operator)value;
            Operator oldOperator = GetCurrentOperator(data);

            if (newOperator != oldOperator)
            {
                switch (newOperator)
                {
                    case Operator.EqualTo:
                        data.Operation = new EqualToOperation<int>();
                        break;
                    case Operator.NotEqualTo:
                        data.Operation = new NotEqualToOperation<int>();
                        break;
                    case Operator.GreaterThan:
                        data.Operation = new GreaterThanOperation<int>();
                        break;
                    case Operator.LessThan:
                        data.Operation = new LessThanOperation<int>();
                        break;
                    case Operator.GreaterThanOrEqual:
                        data.Operation = new GreaterOrEqualOperation<int>();
                        break;
                    case Operator.LessThanOrEqual:
                        data.Operation = new LessThanOrEqualOperation<int>();
                        break;
                }

                changeValueCallback(data);
            }
        }

        private Operator GetCurrentOperator(NumPadCompareCondition.EntityData data)
        {
            Operator currentOperator = Operator.EqualTo;

            if (data.Operation.GetType() == typeof(EqualToOperation<int>))
            {
                currentOperator = Operator.EqualTo;
            }
            else if (data.Operation.GetType() == typeof(NotEqualToOperation<int>))
            {
                currentOperator = Operator.NotEqualTo;
            }
            else if (data.Operation.GetType() == typeof(GreaterThanOperation<int>))
            {
                currentOperator = Operator.GreaterThan;
            }
            else if (data.Operation.GetType() == typeof(LessThanOperation<int>))
            {
                currentOperator = Operator.LessThan;
            }
            else if (data.Operation.GetType() == typeof(GreaterOrEqualOperation<int>))
            {
                currentOperator = Operator.GreaterThanOrEqual;
            }
            else if (data.Operation.GetType() == typeof(LessThanOrEqualOperation<int>))
            {
                currentOperator = Operator.LessThanOrEqual;
            }

            return currentOperator;
        }

    }
}