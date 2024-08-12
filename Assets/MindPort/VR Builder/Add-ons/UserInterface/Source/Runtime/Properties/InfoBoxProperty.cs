using VRBuilder.Core.Properties;
using UnityEngine;
using VRBuilder.UI.Interaction;

namespace VRBuilder.UI.Properties
{
    /// <summary>
    /// <see cref="IInfoBoxProperty{T}"/> implementation with InfoBoxController
    /// </summary>
    [RequireComponent(typeof(InfoBoxController))]
    public class InfoBoxProperty : ProcessSceneObjectProperty, IInfoBoxProperty
    {
        protected InfoBoxController InfoBoxController
        {
            get
            {
                if (infoBoxController == null)
                {
                    infoBoxController = GetComponent<InfoBoxController>();
                }
                return infoBoxController;
            }
        }
        private InfoBoxController infoBoxController;

        public bool IsCompleted => (InfoBoxController != null ? InfoBoxController.IsCompleted : false);
        public bool IsValid => InfoBoxController != null;
        public InfoBoxType InfoBoxType => InfoBoxController != null ? InfoBoxController.Type : InfoBoxType.InfoPanel;

        public void SetInfoBoxType(InfoBoxType type)
        {
            if (InfoBoxController != null)
                InfoBoxController.SetInfoBoxType(type);
        }

        public void ShowInfoBox(bool show, float delay = 0f)
        {
            if (InfoBoxController != null)
                InfoBoxController.ShowInfoBox(show, delay);
            else
                Debug.LogError("InfoBoxProperty: " + this.gameObject.name + " - PreviewInfoBoxController not found!");
        }

        public void SetButtonText(string buttonText)
        {
            if (InfoBoxController != null)
                InfoBoxController.SetButtonText(buttonText);
        }
    }
}
