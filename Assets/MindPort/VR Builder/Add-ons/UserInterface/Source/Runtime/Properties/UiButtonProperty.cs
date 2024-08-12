using VRBuilder.Core.Properties;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VRBuilder.UI.Properties
{
    /// <summary>
    /// <see cref="IUiButtonProperty{T}"/> implementation for UI Buttons and Toggles
    /// </summary>
    [RequireComponent(typeof(Selectable))]
    public class UiButtonProperty : LockableProperty, IUiButtonProperty
    {
        protected Button InteractableButton
        {
            get
            {
                if (interactableButton == false)
                {
                    interactableButton = GetComponent<Button>();
                }

                return interactableButton;
            }
        }
        private Button interactableButton;

        protected Toggle InteractableToggle
        {
            get
            {
                if (interactableToggle == false)
                {
                    interactableToggle = GetComponent<Toggle>();
                }

                return interactableToggle;
            }
        }
        private Toggle interactableToggle;

        public bool InvisibleWhileLocked = false;

        public event EventHandler<EventArgs> OnClicked;

        protected bool isClicked = false;

        /// <summary>
        /// Returns true if the GameObject is being used.
        /// </summary>
        public virtual bool IsClicked
        {
            get { return isClicked; }
            set { isClicked = value; }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (InteractableButton)
                InteractableButton.onClick.AddListener(OnButtonClick);

            if (InteractableToggle)
                InteractableToggle.onValueChanged.AddListener(OnToggleValueChanged);

            InternalSetLocked(IsLocked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (InteractableToggle)
                InteractableToggle.onValueChanged.RemoveListener(OnToggleValueChanged);

            if (InteractableButton)
                InteractableButton.onClick.RemoveListener(OnButtonClick);
        }

        private void OnToggleValueChanged(bool isToggled)
        {
            isClicked = isToggled;
            OnClicked?.Invoke(this, new ToggleEventArgs { IsToggled = isToggled });
        }

        private void OnButtonClick()
        {
            isClicked = true;
            OnClicked?.Invoke(this, EventArgs.Empty);
        }

        protected override void InternalSetLocked(bool lockState)
        {
            if (!LockOnParentObjectLock)
                return;

            Selectable selectable;
            if (InteractableButton)
                selectable = InteractableButton;
            else
                selectable = InteractableToggle;

            if (selectable != null)
            {
                selectable.interactable = lockState == false;
                if (InvisibleWhileLocked)
                {
                    selectable.enabled = lockState == false;
                    if (selectable.targetGraphic != null)
                        selectable.targetGraphic.enabled = lockState == false;
                }
            }
            else
            {
                Debug.LogError("InternalSetLocked: XTUiButtonProperty on " + gameObject.name + " has no valid selectable (Toggle or Button)");
            }
        }

        /// <summary>
        /// Instantaneously simulate that the object was used.
        /// </summary>
        public void FastForwardClick()
        {
            if (InteractableToggle)
                InteractableToggle.onValueChanged?.Invoke(true);

            if (InteractableButton)
                InteractableButton.onClick?.Invoke();
        }

        /// <summary>
        /// UI Button and Toggle are supported
        /// </summary>
        public void SetText(string text)
        {
            if (InteractableToggle)
            {
                var uiText = InteractableToggle.GetComponentInChildren<Text>();
                if (uiText != null) { uiText.text = text;}
                InteractableToggle.GetComponentInChildren<TMP_Text>()?.SetText(text);
            }

            if (InteractableButton)
            {
                var uiText = InteractableButton.GetComponentInChildren<Text>();
                if (uiText != null) { uiText.text = text; }
                InteractableButton.GetComponentInChildren<TMP_Text>()?.SetText(text);
            }
        }
    }

    internal class ToggleEventArgs : EventArgs
    {
        public bool IsToggled { get; set; }
    }
}