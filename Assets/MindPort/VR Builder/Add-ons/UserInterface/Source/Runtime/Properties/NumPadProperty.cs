using UnityEngine;
using VRBuilder.Core.Properties;
using VRBuilder.UI.Interaction;

namespace VRBuilder.UI.Properties
{
    /// <summary>
    /// <see cref="INumPadProperty{T}"/> implementation with NumPadController
    /// </summary>
    [RequireComponent(typeof(NumPadController))]
    public class NumPadProperty : ProcessSceneObjectProperty, INumPadProperty
    {
        protected NumPadController NumPad
        {
            get
            {
                if (!_numPad) _numPad = GetComponent<NumPadController>();
                return _numPad;
            }
        }

        private NumPadController _numPad;

        private int? _finalValue;


        public bool IsDataAccepted()
        {
            return _finalValue.HasValue;
        }

        public bool IsValid => NumPad != null;

        public bool IsShowing()
        {
            if (NumPad.MainPanel != null)
            {
                return NumPad.MainPanel.gameObject.activeSelf;
            }
            else
            {
                return NumPad.gameObject.activeSelf;
            }
        }
        public void SetNumPadVisibility(bool visible)
        {
            if (NumPad.MainPanel != null)
            {
                NumPad.MainPanel.gameObject.SetActive(visible);
            }
            else
            {
                NumPad.gameObject.SetActive(visible);
            }
        }

        public void ResetNumPad(bool resetEnteredValue)
        {
            if(resetEnteredValue) _finalValue = null;

            NumPad.ResetDisplay();
            NumPad.OnAccept.RemoveListener(OnAccept);
        }

        public void InitNumPad()
        {
            NumPad.OnAccept.AddListener(OnAccept);
        }

        private void OnAccept(int finalValue)
        {
            _finalValue = finalValue;
        }

        public int GetValue()
        {
            if (_finalValue != null) return _finalValue.Value;
            return -1;
        }
    }
}