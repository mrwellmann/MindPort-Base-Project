using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.XR.CoreUtils;
using VRBuilder.UI.Utils;
using UnityEngine.EventSystems;

namespace VRBuilder.UI.Interaction
{
    public class MultipleChoiceController : MonoBehaviour
    {
        public TextMeshProUGUI Title;
        public Canvas MainCanvas;
        public Transform RootPanel;

        public Transform GridLayoutSettings2;
        public Transform GridLayoutSettings4;
        public Transform GridLayoutSettings12;
        public Transform GridLayoutSettingsVertical;

        public GameObject ButtonPrefab;
        public bool UseVerticalLayout = false;

        public bool SetTextSizeToSmallestSize = true;

        protected int hideDelay { get; set; } 

        protected readonly List<GameObject> createdButtons = new List<GameObject>();
        protected readonly List<TextMeshProUGUI> buttonTextMeshes = new List<TextMeshProUGUI>();
        protected string pressedButtonText = null;

        protected void Start()
        {
            if (MainCanvas != null && MainCanvas.worldCamera == null)
            {
                var rig = FindObjectOfType<XROrigin>();
                if (rig != null)
                    MainCanvas.worldCamera = rig.Camera;
                else
                    MainCanvas.worldCamera = Camera.main;
            }

            RootPanel.gameObject.SetActive(false);
        }

        public void SetVerticalButtonLayout(bool enabled)
        {
            this.UseVerticalLayout = enabled;
            HandleButtonLayout();
        }

        public bool IsShowing()
        {
            return RootPanel.gameObject.activeSelf;
        }

        public void Show()
        {
            CancelInvoke(nameof(DisableRootPanel));
            CancelInvoke(nameof(TrySetTextSizeToSmallestSize));
            RootPanel.gameObject.SetActive(true);
            Invoke(nameof(TrySetTextSizeToSmallestSize), Time.fixedDeltaTime);
        }


        public void Hide()
        {
            if (hideDelay > 0)
            {
                DisableButtons();
                Invoke(nameof(DisableRootPanel), hideDelay);
            }
            else
            {
                DisableRootPanel();
                ResetMultipleChoiceBox();
            }
        }

        private void DisableButtons()
        {
            for (int i = 0; createdButtons != null && i < createdButtons.Count; i++)
            {
                Button button = createdButtons[i].GetComponentInChildren<Button>();
                if (EventSystem.current.currentSelectedGameObject == createdButtons[i])
                {
                    ColorBlock colorBlock = button.colors;
                    colorBlock.normalColor = button.colors.normalColor;
                    colorBlock.selectedColor = button.colors.selectedColor;
                    colorBlock.highlightedColor = button.colors.highlightedColor;
                    colorBlock.disabledColor = button.colors.highlightedColor;
                    button.colors = colorBlock;
                }
                button.interactable = false;
            }
        }

        private void DisableRootPanel()
        {
            RootPanel.gameObject.SetActive(false);
        }

        public void SetTitle(string text)
        {
            Title.text = text;
        }

        public void ResetMultipleChoiceBox()
        {
            for (int i = 0; createdButtons != null && i < createdButtons.Count; i++)
            {
                Destroy(createdButtons[i].gameObject);
            }
            createdButtons.Clear();
            buttonTextMeshes.Clear();

            GridLayoutSettings2.gameObject.SetActive(false);
            GridLayoutSettings4.gameObject.SetActive(false);
            GridLayoutSettings12.gameObject.SetActive(false);
            GridLayoutSettingsVertical.gameObject.SetActive(false);

            pressedButtonText = null;
        }

        public void CreateMultipleChoiceButton(string spriteKey, Sprite sprite)
        {
            var buttonObject = GameObject.Instantiate(ButtonPrefab);
            if (buttonObject != null)
            {
                HandleButtonObject(buttonObject, spriteKey, sprite);
                AddCreatedButton(buttonObject);
                HandleButtonLayout();
            }
        }

        public void CreateMultipleChoiceButton(string text)
        {
            var buttonObject = GameObject.Instantiate(ButtonPrefab);
            if (buttonObject != null)
            {
                HandleButtonObject(buttonObject, text);
                AddCreatedButton(buttonObject);
                HandleButtonLayout();
            }
        }

        public void SetHideDelay(int seconds)
        {
            hideDelay = seconds;
        }

        protected void AddCreatedButton(GameObject buttonObject)
        {
            if (!createdButtons.Contains(buttonObject))
            {
                createdButtons.Add(buttonObject);
                var buttonTextMesh = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonTextMesh != null) buttonTextMeshes.Add(buttonTextMesh);
            }
        }

        protected void HandleButtonLayout()
        {
            if (UseVerticalLayout)
            {
                GridLayoutSettings2.gameObject.SetActive(false);
                GridLayoutSettings4.gameObject.SetActive(false);
                GridLayoutSettings12.gameObject.SetActive(false);
                GridLayoutSettingsVertical.gameObject.SetActive(true);
                MoveButtonsToPanel(GridLayoutSettingsVertical);
            }
            else
            {
                var buttonCount = createdButtons.Count;
                if (buttonCount <= 2)
                {
                    GridLayoutSettings2.gameObject.SetActive(true);
                    GridLayoutSettings4.gameObject.SetActive(false);
                    GridLayoutSettings12.gameObject.SetActive(false);
                    GridLayoutSettingsVertical.gameObject.SetActive(false);
                    MoveButtonsToPanel(GridLayoutSettings2);

                }
                else if (buttonCount <= 4)
                {
                    GridLayoutSettings2.gameObject.SetActive(false);
                    GridLayoutSettings4.gameObject.SetActive(true);
                    GridLayoutSettings12.gameObject.SetActive(false);
                    GridLayoutSettingsVertical.gameObject.SetActive(false);
                    MoveButtonsToPanel(GridLayoutSettings4);
                }
                else
                {
                    GridLayoutSettings2.gameObject.SetActive(false);
                    GridLayoutSettings4.gameObject.SetActive(false);
                    GridLayoutSettings12.gameObject.SetActive(true);
                    GridLayoutSettingsVertical.gameObject.SetActive(false);
                    MoveButtonsToPanel(GridLayoutSettings12);
                }
            }
        }
        protected void MoveButtonsToPanel(Transform panel)
        {
            for (int i = 0; i < createdButtons.Count; i++)
            {
                createdButtons[i].transform.SetParent(panel);
                createdButtons[i].transform.localPosition = Vector3.zero;
                createdButtons[i].transform.localRotation = Quaternion.identity;
                createdButtons[i].transform.localScale = Vector3.one;
            }
        }

        protected void TrySetTextSizeToSmallestSize()
        {
            if (SetTextSizeToSmallestSize)
                AutoSizeButtonTextMeshes(buttonTextMeshes.ToArray());
        }

        private void AutoSizeButtonTextMeshes(TextMeshProUGUI[] buttonTextMeshes)
        {
            if (buttonTextMeshes == null || buttonTextMeshes.Length == 0)
                return;

            float optimumPointSize = float.MaxValue;
            for (int i = 0; i < buttonTextMeshes.Length; i++)
            {
                if (buttonTextMeshes[i].fontSize < optimumPointSize)
                {
                    optimumPointSize = buttonTextMeshes[i].fontSize;
                }
            }

            for (int i = 0; i < buttonTextMeshes.Length; i++)
            {
                buttonTextMeshes[i].enableAutoSizing = false;
                buttonTextMeshes[i].fontSize = optimumPointSize;
            }
        }

        public bool HasPressedButton(string text)
        {
            if (string.IsNullOrEmpty(pressedButtonText))
                return false;

            return pressedButtonText == text;
        }

        protected void HandleButtonObject(GameObject buttonObject, string buttonText)
        {
            var mcb = buttonObject.GetComponent<MultipleChoiceButton>();
            if (mcb != null)
            {
                mcb.SetButtonContent(buttonText);
            }
            else
            {
                var buttonTextField = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonTextField != null)
                {
                    buttonTextField.text = buttonText;
                }
            }

            var button = buttonObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(delegate { OnButtonClick(buttonText); });
            }
        }

        protected void HandleButtonObject(GameObject buttonObject, string spriteKey, Sprite sprite)
        {
            var mcb = buttonObject.GetComponent<MultipleChoiceButton>();
            if (mcb != null)
            {
                mcb.SetButtonContent(sprite);
            }

            var button = buttonObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(delegate { OnButtonClick(spriteKey); });
            }
        }

        protected void OnButtonClick(string buttonText)
        {
            pressedButtonText = buttonText;
        }

    }
}
