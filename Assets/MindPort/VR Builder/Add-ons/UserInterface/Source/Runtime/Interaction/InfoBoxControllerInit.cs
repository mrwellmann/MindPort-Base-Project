using UnityEngine;

namespace VRBuilder.UI.Interaction
{
    [ExecuteInEditMode]
    public class InfoBoxControllerInit : MonoBehaviour
    {
        void Awake()
        {
            var infoBoxController = GetComponent<InfoBoxController>();
            if (infoBoxController != null)
            {
                infoBoxController.InitComponents();
            }
        }
    }
}


