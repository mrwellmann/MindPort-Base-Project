using VRBuilder.Core.Properties;
using UnityEngine;
using UnityEngine.Localization;
using VRBuilder.UI.Interaction;

namespace VRBuilder.UI.Properties
{
    /// <summary>
    /// <see cref="IMultipleChoiceProperty{T}"/> implementation with MultipleChoiceController
    /// </summary>
    [RequireComponent(typeof(MultipleChoiceController))]
    public class MultipleChoiceProperty : ProcessSceneObjectProperty, IMultipleChoiceProperty
    {
        protected MultipleChoiceController MultipleChoiceBox
        {
            get
            {
                if (multipleChoiceBox == null)
                {
                    multipleChoiceBox = GetComponent<MultipleChoiceController>();
                }
                return multipleChoiceBox;
            }
        }
        private MultipleChoiceController multipleChoiceBox;

        public bool IsValid => MultipleChoiceBox != null;

        public void SetVerticalButtonLayout(bool enabled)
        {
            MultipleChoiceBox.SetVerticalButtonLayout(enabled);
        }
        public void CreateMultipleChoiceButton(string loclizationTable, string text, bool isSprite)
        {
            if (string.IsNullOrEmpty(loclizationTable))
            {
                CreateMultipleChoiceButton(text, isSprite);
            }
            else
            {
                if (isSprite)
                {
                    MultipleChoiceBox.CreateMultipleChoiceButton(text, TryGetLocalizedSprite(loclizationTable, text));
                }
                else
                {
                    MultipleChoiceBox.CreateMultipleChoiceButton(TryGetLocalizedString(loclizationTable, text));
                }
            }
        }

        public void CreateMultipleChoiceButton(string text, bool isSprite)
        {
            if (isSprite)
            {
                var sprite = Resources.Load<Sprite>(text);
                if (sprite != null)
                {
                    MultipleChoiceBox.CreateMultipleChoiceButton(text, sprite);
                }
                else
                {
                    MultipleChoiceBox.CreateMultipleChoiceButton(text);
                }
            }
            else
            {
                MultipleChoiceBox.CreateMultipleChoiceButton(text);
            }
        }

        public void ShowMultipleChoiceBox(float delay = 0)
        {
            CancelInvoke(nameof(ShowMultipleChoiceBox));

            if (delay > 0f)
            {
                Invoke(nameof(ShowMultipleChoiceBox), delay);
            }
            else
            {
                ShowMultipleChoiceBox();
            }
        }

        public void ShowMultipleChoiceBox()
        {
            MultipleChoiceBox.Show();
        }

        public void HideMultipleChoiceBox()
        {
            CancelInvoke(nameof(ShowMultipleChoiceBox));
            MultipleChoiceBox.Hide();
        }

        public void ResetMultipleChoiceBox()
        {
            MultipleChoiceBox.ResetMultipleChoiceBox();
        }

        public void SetMultipleChoiceBoxTitle(string localizationTable, string text)
        {
            SetMultipleChoiceBoxTitle(TryGetLocalizedString(localizationTable, text));
        }

        public void SetMultipleChoiceBoxTitle(string text)
        {
            MultipleChoiceBox.SetTitle(text);
        }

        public bool IsButtonPressed(string localizationTable, string text, bool isSprite)
        {
            if (isSprite)
            {
                return IsButtonPressed(text);
            }
            else
            {
                return IsButtonPressed(TryGetLocalizedString(localizationTable, text));
            }
        }

        public bool IsButtonPressed(string text)
        {
            return MultipleChoiceBox.HasPressedButton(text);
        }

        public void SetHideDelay(int seconds)
        {
            MultipleChoiceBox.SetHideDelay(seconds);
        }

        protected string TryGetLocalizedString(string localizationTable, string text)
        {
            if (!string.IsNullOrEmpty(localizationTable))
            {
                LocalizedString localizedString = new LocalizedString(localizationTable, text);
                if (!localizedString.IsEmpty)
                {
                    return localizedString.GetLocalizedString();
                }
            }
            return text;
        }

        protected Sprite TryGetLocalizedSprite(string localizationTable, string text)
        {
            if (!string.IsNullOrEmpty(localizationTable))
            {
                LocalizedSprite localizedSprite = new LocalizedSprite();
                localizedSprite.SetReference(localizationTable, text);
                return localizedSprite.LoadAsset();
                
            }
            return null;
        }
    }
}
