using UnityEngine;
using Unity.XR.CoreUtils;

namespace VRBuilder.UI.Utils
{
    [RequireComponent(typeof(Canvas))]
    public class AutoSetCameraToCanvas : MonoBehaviour
    {
        protected Canvas myCanvas;

        private void Start()
        {
            if (myCanvas == null)
                myCanvas = GetComponent<Canvas>();

            Camera myCamera = null;

            XROrigin xrRig = FindObjectOfType<XROrigin>();
            if (xrRig!=null)
                myCamera = xrRig.Camera;

            if (myCamera == null)
                myCamera = Camera.main;

            if (myCanvas != null && myCanvas.worldCamera == null)
                myCanvas.worldCamera = myCamera;
        }
    }
}
