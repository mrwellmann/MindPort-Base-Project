using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VRBuilder.UI.Interaction
{
    /// <summary>
    /// Use to drive hand animations.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class UiHandAnimatorController : MonoBehaviour
    {
        [Header("Animator Parameters")]
        [SerializeField]
        [Tooltip("Float parameter corresponding to select value.")]        
        private string selectFloat = "Select";

        [SerializeField]
        [Tooltip("Float parameter corresponding to activate value.")]
        private string activateFloat = "Activate";

        [SerializeField]
        [Tooltip("Bool parameter true if UI state enabled.")]
        private string UIStateBool = "UIEnabled";

        [SerializeField]
        [Tooltip("Bool parameter true if teleport state enabled.")]
        private string teleportStateBool = "TeleportEnabled";

        private Animator animator;

        public enum State
        {
            Idle,
            Touch,
            Grab,
            Teleport,
            UI
        }
        protected State currentHandState = State.Idle;

        public XRRayInteractor MyTeleportInteractor;
        public XRRayInteractor MyUIInteractor;
        public XRController MyController;


        private void Start()
        {
            animator = GetComponent<Animator>();

            if (MyTeleportInteractor != null)
            {
                MyTeleportInteractor.hoverEntered.AddListener(OnEnterTeleport);
                MyTeleportInteractor.hoverExited.AddListener(OnExitTeleport);
            }

            if (MyUIInteractor != null)
            {
                MyUIInteractor.hoverEntered.AddListener(OnEnterUI);
                MyUIInteractor.hoverExited.AddListener(OnExitUI);
            }
        }

        private void Update()
        {
            if (currentHandState == State.Idle && MyController != null)
            {
                if (MyController.selectInteractionState.activatedThisFrame)
                {
                    animator.SetFloat(selectFloat, 1f);
                }
                else if (MyController.activateInteractionState.activatedThisFrame)
                {
                    animator.SetFloat(activateFloat, 1f);
                }
                else if (MyController.selectInteractionState.deactivatedThisFrame ||
                         MyController.activateInteractionState.deactivatedThisFrame)
                {
                    animator.SetFloat(selectFloat, 0f);
                    animator.SetFloat(activateFloat, 0f);
                }
            }
        }

        public void OnEnterUI()
        {
            if (currentHandState == State.Idle)
            {
                currentHandState = State.UI;
                animator.SetBool(UIStateBool, true);
            }
        }

        private void OnEnterUI(HoverEnterEventArgs arguments)
        {
            OnEnterUI();
        }

        public void OnExitUI()
        {
            if (currentHandState == State.UI)
            {
                currentHandState = State.Idle;
                animator.SetBool(UIStateBool, false);
            }
        }

        protected void OnExitUI(HoverExitEventArgs arguments)
        {
            OnExitUI();
        }

        protected void OnEnterTeleport(HoverEnterEventArgs arguments)
        {
            if (currentHandState == State.Idle)
            {
                currentHandState = State.Teleport;
                animator.SetBool(teleportStateBool, true);
            }
        }

        protected void OnExitTeleport(HoverExitEventArgs arguments)
        {
            if (currentHandState == State.Teleport)
            {
                currentHandState = State.Idle;
                animator.SetBool(teleportStateBool, false);
            }
        }
    }
}