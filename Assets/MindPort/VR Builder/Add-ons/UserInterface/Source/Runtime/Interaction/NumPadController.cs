using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VRBuilder.UI.Utils;

namespace VRBuilder.UI.Interaction
{
    public class NumPadController : MonoBehaviour
    {
        public TMP_Text Display;
        public RectTransform MainPanel;

        public Button[] Buttons;

        public UnityEvent<int> OnAccept = new();

        private void OnEnable()
        {
            foreach (var button in Buttons)
                button.onClick.AddListener(() => OnButtonClicked(button));
        }

        private void OnDisable()
        {
            foreach (var button in Buttons)
            {
                button.onClick.RemoveAllListeners();
            }
        }

        private void OnButtonClicked(Component eventSource)
        {
            string btText = eventSource.GetComponentInChildren<TMP_Text>().text;
            if (eventSource.TryGetComponent(typeof(NumPadTagDelete), out _))
            {
                if (Display != null && !string.IsNullOrEmpty(Display.text))
                {
                    Display.text = "";
                }
            }
            else if (eventSource.TryGetComponent(typeof(NumPadTagAccept), out _))
            {
                if (Display != null && !string.IsNullOrEmpty(Display.text))
                {
                    var value = 0;
                    try
                    {
                        value = int.Parse(Display.text);
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning(e);
                    }

                    OnAccept?.Invoke(value);
                }
            }
            else
            {
                if (Display != null)
                {
                    Display.text += btText;
                }
            }
        }

        public void ResetDisplay()
        {
            if (Display != null && !string.IsNullOrEmpty(Display.text))
            {
                Display.text = "";
            }
        }
    }
}