using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;
using VRBuilder.UI.Behaviors;

namespace VRBuilder.Editor.UI.Drawers
{
    ///<author email="a.schaub@lefx.de">Aron Schaub</author>
    [DefaultProcessDrawer(typeof(SetLocalizedTextBehavior.EntityData))]
    public class LocalizedStringPropertyDrawer : ObjectDrawer
    {
        private const string NONE = "None";
        private bool initialized = false;
        private string[] collections;

        public override Rect Draw(Rect rect, object currentValue, Action<object> changeValueCallback, GUIContent label)
        {
            rect = base.Draw(rect, currentValue, changeValueCallback, label);
            float height = rect.height;
            height += EditorDrawingHelper.VerticalSpacing;
            Rect nextPosition = new Rect(rect.x, rect.y + height, rect.width, rect.height);
            if (!initialized)
            {
                Initialize();
            }

            if (currentValue is SetLocalizedTextBehavior.EntityData data)
            {
                int idx = collections.Select((s, i) => new { i, s })
                    .Where(t => t.s == data.LocalizationTable)
                    .Select(t => t.i)
                    .FirstOrDefault();
                var memberInfo = data.GetType().GetField(nameof(data.LocalizationTable));
                string newValue = collections[EditorGUI.Popup(nextPosition, memberInfo.GetDisplayName(), idx, collections)];
                if (newValue != data.LocalizationTable)
                {
                    data.LocalizationTable = newValue;
                    changeValueCallback(data);
                }
            }

            nextPosition = new Rect(rect.x, rect.y + height, rect.width, rect.height + EditorGUIUtility.singleLineHeight);
            return nextPosition;
        }

        protected override IEnumerable<MemberInfo> GetMembersToDraw(object value)
        {
            if (value is SetLocalizedTextBehavior.EntityData data)
                return base.GetMembersToDraw(value).Where(info =>
                {
                    bool b = true;
                    // bool b = !(info.Name == nameof(data.Text) && data.LocalizationTable == NONE);
                    b &= info.Name != nameof(data.LocalizationTable);
                    return b;
                });
            return base.GetMembersToDraw(value);
        }

        private void Initialize()
        {
            List<string> l = LocalizationEditorSettings.GetStringTableCollections().Select(t => t.TableCollectionName).ToList();
            l.Insert(0, NONE);
            collections = l.ToArray();

            initialized = true;
        }
    }
}