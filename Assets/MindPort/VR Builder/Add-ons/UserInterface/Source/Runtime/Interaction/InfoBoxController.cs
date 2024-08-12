using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System;
using VRBuilder.UI.Properties;

namespace VRBuilder.UI.Interaction
{
    [SelectionBase]
    public class InfoBoxController : MonoBehaviour
    {
        public InfoBoxType Type = InfoBoxType.Mandatory;

        public string LocalizationTable;
        public string Title;
        public string Description;
        public string DescriptionTitle;

        public RectTransform PreviewInfoBoxCanvas;

        public Image TitleIcon;
        public Sprite TitleSprite;
        public TextMeshProUGUI TitleText;
        public RectTransform ExitButtonTransform;

        public RectTransform DetailsContainerTransform;
        public TextMeshProUGUI DetailsTitleText;
        public TextMeshProUGUI DetailsDescriptionText;

        public bool ScaleDetailsContainerWithTextSize = true;
        public float ScaleDetailsContainerTextSizeFactor = 0.5f;

        public RectTransform ImageContainerTransform;
        public Image ImageIcon;
        public Sprite ImageSprite;
        public Color ImageColor = Color.white;

        public RectTransform AcknowledgeButtonTransform;
        public TextMeshProUGUI AcknowledgeButtonText;

        [Range(0.1f, 10f)]
        public float InfoBoxSize = 1f;
        public bool IsCompleted { get; protected set; }

        protected void Awake()
        {
            PreviewInfoBoxCanvas.gameObject.SetActive(false);

            if (Type == InfoBoxType.InfoPanel)
            {
                PreviewInfoBoxCanvas.gameObject.SetActive(true);
            }
            else
            {
                PreviewInfoBoxCanvas.gameObject.SetActive(GetComponent<IInfoBoxProperty>() == null);
            }
        }

        protected void OnEnable()
        {
            SetTexts(LocalizationTable, Title, Description);
            SetDescriptionTitle(LocalizationTable, DescriptionTitle);
            LocalizationSettings.SelectedLocaleChanged += OnSelectedLoaclechanged;
        }

        protected void OnDisable()
        {
            LocalizationSettings.SelectedLocaleChanged -= OnSelectedLoaclechanged;
        }

        private void OnSelectedLoaclechanged(Locale obj)
        {
            if (!string.IsNullOrEmpty(LocalizationTable))
            {
                SetTexts(LocalizationTable, Title, Description);
                SetDescriptionTitle(LocalizationTable, DescriptionTitle);
            }
        }

        public void InitComponents()
        {
            SetInfoBoxType(Type);
            SetTexts(LocalizationTable, Title, Description);
            SetImage(ImageSprite, ImageColor);
            SetInfoBoxSize(InfoBoxSize);
        }

        public void SetTitleImage(Sprite titleSprite)
        {
            TitleIcon.sprite = titleSprite;
        }

        public void SetTitleImage(string path)
        {
            var sprite = Resources.Load<Sprite>(path);
            if (sprite != null)
                SetTitleImage(sprite);
        }

        public void EnableExitButton(bool enabled)
        {
            var components = ExitButtonTransform.GetComponentsInChildren<MonoBehaviour>(true);
            for (int i = 0; i < components.Length; i++)
            {
                components[i].enabled = enabled;
            }
        }

        public void OnClickExitButton()
        {
            PreviewInfoBoxCanvas.gameObject.SetActive(false);
        }

        public void ActivateDetailsContainer(bool active)
        {
            DetailsContainerTransform.gameObject.SetActive(active);
        }

        public void SetDescriptionTitle(string localizationTable, string descriptionTitle)
        {
            if (!string.IsNullOrEmpty(descriptionTitle) &&
                !string.IsNullOrEmpty(localizationTable) &&
                LocalizationSettings.SelectedLocale != null &&
                LocalizationSettings.StringDatabase.GetTable(localizationTable, LocalizationSettings.SelectedLocale) != null)
            {
                LocalizedString localizedDescriptionTitle = new LocalizedString(localizationTable, descriptionTitle);
                if (localizedDescriptionTitle != null && !localizedDescriptionTitle.IsEmpty)
                {
                    DetailsTitleText.text = localizedDescriptionTitle.GetLocalizedString();
                }
                else
                {
                    DetailsTitleText.text = descriptionTitle;
                }
            }
            else
            {
                DetailsTitleText.text = descriptionTitle;
            }
        }

        public void SetDetailsDescriptionText(string text)
        {
            DetailsDescriptionText.text = text;

            if (ScaleDetailsContainerWithTextSize && !string.IsNullOrEmpty(text))
            {
                DetailsContainerTransform.sizeDelta = new Vector2(DetailsContainerTransform.sizeDelta.x,
                    Mathf.Min(512, Mathf.Max(256, text.Length * ScaleDetailsContainerTextSizeFactor)));
            }
        }

        public void ActivateImageContainer(bool active)
        {
            ImageContainerTransform.gameObject.SetActive(active);
        }

        public void SetImage(Sprite imageSprite, Color imageColor)
        {
            if (imageSprite != null)
            {
                SetImpageIcon(imageSprite, imageColor);
                ActivateImageContainer(true);
            }
            else
            {
                ActivateImageContainer(false);
            }
        }

        public void SetImpageIcon(Sprite sprite, Color color)
        {
            ImageIcon.color = color;
            ImageIcon.sprite = sprite;
        }

        public void ActivateAckButtonContainer(bool active)
        {
            AcknowledgeButtonTransform.gameObject.SetActive(active);
        }

        public void OnClickAcknowledgeButton()
        {
            IsCompleted = true;
            PreviewInfoBoxCanvas.gameObject.SetActive(false);
        }

        public void OnClickOpenPanelButton()
        {
            PreviewInfoBoxCanvas.gameObject.SetActive(true);
        }

        //IInfoBoxProperty Impl
        public void SetInfoBoxType(InfoBoxType type)
        {
            Type = type;

            switch (type)
            {
                case InfoBoxType.Mandatory:
                    EnableExitButton(false);
                    ActivateAckButtonContainer(true);
                    break;
                case InfoBoxType.Optional:
                    EnableExitButton(true);
                    ActivateAckButtonContainer(false);
                    break;
                case InfoBoxType.InfoPanel:
                    EnableExitButton(false);
                    ActivateAckButtonContainer(false);
                    break;
            }
        }

        //IInfoBoxProperty Impl
        public void SetTexts(string localizationTable, string title, string description)
        {
            if (!string.IsNullOrEmpty(localizationTable) &&
                LocalizationSettings.SelectedLocale != null &&
                LocalizationSettings.StringDatabase.GetTable(localizationTable, LocalizationSettings.SelectedLocale) != null)
            {
                LocalizedString localizedTitle = new LocalizedString(localizationTable, title);
                if (localizedTitle != null && !localizedTitle.IsEmpty)
                {
                    TitleText.text = localizedTitle.GetLocalizedString();
                }
                else
                {
                    TitleText.text = title;
                }

                LocalizedString localizedDescription = string.IsNullOrEmpty(description) ? null : new LocalizedString(localizationTable, description);
                if (localizedDescription != null && !localizedDescription.IsEmpty)
                {
                    ActivateDetailsContainer(true);
                    SetDetailsDescriptionText(localizedDescription.GetLocalizedString());
                }
                else
                {
                    ActivateDetailsContainer(false);
                    SetDetailsDescriptionText(description);
                }
            }
            else
            {
                TitleText.text = title;

                if (!string.IsNullOrEmpty(description))
                {
                    ActivateDetailsContainer(true);
                    SetDetailsDescriptionText(description);
                }
                else
                {
                    ActivateDetailsContainer(false);
                    SetDetailsDescriptionText(description);
                }
            }
        }

        //IInfoBoxProperty Impl
        public void SetButtonText(string buttonText)
        {
            if (AcknowledgeButtonText != null)
                AcknowledgeButtonText.text = buttonText;
        }

        //IInfoBoxProperty Impl
        public void ShowInfoBox(bool show, float delay = 0f)
        {
            CancelInvoke(nameof(HideInfoBox));
            CancelInvoke(nameof(ShowInfoBox));

            if (!show)
            {
                HideInfoBox();
            }
            else if (delay > 0f)
            {
                Invoke(nameof(ShowInfoBox), delay);
            }
            else
            {
                ShowInfoBox();
            }
        }

        //IInfoBoxProperty Impl
        public void ShowInfoBox()
        {
            switch (Type)
            {
                case InfoBoxType.Mandatory:
                    IsCompleted = false;
                    PreviewInfoBoxCanvas.gameObject.SetActive(true);
                    break;
                case InfoBoxType.Optional:
                    PreviewInfoBoxCanvas.gameObject.SetActive(true);
                    break;
                case InfoBoxType.InfoPanel:
                    PreviewInfoBoxCanvas.gameObject.SetActive(true);
                    break;
            }
        }

        //IInfoBoxProperty Impl
        public void HideInfoBox()
        {
            PreviewInfoBoxCanvas.gameObject.SetActive(false);
        }

        public void SetInfoBoxSize(float infoBoxSize)
        {
            if (PreviewInfoBoxCanvas != null)
                PreviewInfoBoxCanvas.localScale = Vector3.one * (infoBoxSize * 0.001f);
        }

        public bool ValidateInfoBoxEnlarge()
        {
            return !string.IsNullOrEmpty(Description) || ImageSprite != null;
        }

        public string GetLocalizedTitle()
        {
            if (!string.IsNullOrEmpty(LocalizationTable) &&
                LocalizationSettings.SelectedLocale != null &&
                LocalizationSettings.StringDatabase.GetTable(LocalizationTable, LocalizationSettings.SelectedLocale) != null)
            {
                LocalizedString localizedTitle = new LocalizedString(LocalizationTable, Title);
                if (localizedTitle != null && !localizedTitle.IsEmpty)
                {
                    return localizedTitle.GetLocalizedString();
                }
            }
            return Title;
        }

        public string GetLocalizedDescription()
        {
            if (!string.IsNullOrEmpty(LocalizationTable) &&
                LocalizationSettings.SelectedLocale != null &&
                LocalizationSettings.StringDatabase.GetTable(LocalizationTable, LocalizationSettings.SelectedLocale) != null)
            {
                LocalizedString localizedDescription = string.IsNullOrEmpty(Description) ? null : new LocalizedString(LocalizationTable, Description);
                if (localizedDescription != null && !localizedDescription.IsEmpty)
                {
                    return localizedDescription.GetLocalizedString();
                }
            }

            return Description;
        }
    }
}


