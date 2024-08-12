using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using VRBuilder.Core.Properties;

namespace VRBuilder.UI.Properties
{
    /// <summary>
    /// <see cref="ILocalizedTextProperty{T}"/> implementation for TextMesh, Text and TextMeshPro (TMP) Compontents
    /// </summary>
    public class LocalizedTextProperty : LockableProperty, ILocalizedTextProperty
    {
        protected TMP_Text TextMeshPro
        {
            get
            {
                if (textMeshPro == null)
                {
                    textMeshPro = GetComponentInChildren<TMP_Text>();
                }
                return textMeshPro;
            }
        }
        private TMP_Text textMeshPro;

        protected TextMesh LegacyTextMesh
        {
            get
            {
                if (legacyTextMesh == null)
                {
                    legacyTextMesh = GetComponentInChildren<TextMesh>();
                }
                return legacyTextMesh;
            }
        }
        private TextMesh legacyTextMesh;

        protected Text LegacyText
        {
            get
            {
                if (legacyText == null)
                {
                    legacyText = GetComponentInChildren<Text>();
                }
                return legacyText;
            }
        }
        private Text legacyText;

        public void AppendText(string localizationTable, string text)
        {
            string localizedText = GetLocalizedText(localizationTable, text);
            if (TextMeshPro != null) TextMeshPro.text += localizedText;
            if (LegacyText != null) LegacyText.text += localizedText;
            if (LegacyTextMesh != null) LegacyTextMesh.text += localizedText;
        }

        public void SetText(string localizationTable, string text)
        {
            string localizedText = GetLocalizedText(localizationTable, text);
            if(TextMeshPro!=null) TextMeshPro.text = localizedText;
            if(LegacyText!=null) LegacyText.text = localizedText;
            if(LegacyTextMesh!=null) LegacyTextMesh.text = localizedText;
        }

        protected string GetLocalizedText(string localizationTable, string text)
        {
            if (!string.IsNullOrEmpty(localizationTable) &&
                LocalizationSettings.SelectedLocale != null &&
                LocalizationSettings.StringDatabase.GetTable(localizationTable, LocalizationSettings.SelectedLocale) != null)
            {
                LocalizedString localizedText = new LocalizedString(localizationTable, text);
                if (localizedText != null && !localizedText.IsEmpty)
                {
                    return localizedText.GetLocalizedString();
                }
            }
            return text;
        }

        protected override void InternalSetLocked(bool lockState)
        {
        }
    }
}
