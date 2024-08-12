using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

#if VR_BUILDER_XR_INTERACTION
using VRBuilder.XRInteraction;
using VRBuilder.XRInteraction.Animation;
using static VRBuilder.XRInteraction.ControllerManager;
#endif

namespace VRBuilder.UI.Interaction
{
    /// <summary>
    /// Raycast Helper for Detecting UI Elements, to enable Near Touch UI Interactors
    /// </summary>
    [RequireComponent(typeof(XRRayInteractor))]
    public class UiDetectionRaycast : MonoBehaviour
    {

#if VR_BUILDER_XR_INTERACTION

        public UiHandAnimatorController[] HandAnimationControllers;

        public DirectInteractor[] DirectInteractors;

        public XRRayInteractor[] TeleportInteractors;
        public Transform[] UiInteractorControllers;

        protected List<InteractorController> uiControllers = new List<InteractorController>();
        protected bool uiControllersEnabled = false;

        protected XRRayInteractor myRayInteractor;

        Vector3 m_ReticlePos = new Vector3(), m_ReticleNormal = new Vector3();
        int m_EndPositionInLine = 0;

        protected void Start()
        {
            if (myRayInteractor == null)
                myRayInteractor = GetComponent<XRRayInteractor>();

            Clear();

            for (int i = 0; i < UiInteractorControllers.Length; i++)
            {
                var controller = new InteractorController();
                controller.Attach(UiInteractorControllers[i].gameObject);
                controller.Leave();
                uiControllers.Add(controller);
            }
        }

        protected void Update()
        {
            if (myRayInteractor == null)
                return;

            bool isValidTarget = false;
            if (ValidateDirectInteractors())
                myRayInteractor.TryGetHitInfo(out m_ReticlePos, out m_ReticleNormal, out m_EndPositionInLine, out isValidTarget);

            if (!isValidTarget && IsControllerInteracting())
                return;

            if (uiControllersEnabled != isValidTarget)
            {
                EnableControllers(isValidTarget);
                uiControllersEnabled = isValidTarget;
            }
        }

        protected void OnDisable()
        {
            Clear();
        }

        protected void EnableControllers(bool enabled)
        {
            for (int i = 0; i < TeleportInteractors.Length; i++)
            {
                TeleportInteractors[i].gameObject.SetActive(!enabled);
            }

            for (int i = 0; i < uiControllers.Count; i++)
            {
                if (enabled)
                    uiControllers[i].Enter();
                else
                    uiControllers[i].Leave();
            }

            for (int i = 0; i < HandAnimationControllers.Length; i++)
            {
                if (enabled)
                    HandAnimationControllers[i].OnEnterUI();
                else
                    HandAnimationControllers[i].OnExitUI();
            }

        }

        protected bool ValidateDirectInteractors()
        {
            for (int i = 0; i < DirectInteractors.Length; i++)
            {
                if (DirectInteractors[i].hasHover || DirectInteractors[i].hasSelection)
                    return false;
            }
            return true;
        }

        protected bool IsControllerInteracting()
        {
            for (int i = 0; i < uiControllers.Count; i++)
            {
                if (uiControllers[i].IsInteractorInteracting())
                    return true;
            }
            return false;
        }

        protected void Clear()
        {
            if (uiControllers.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < uiControllers.Count; ++i)
            {
                uiControllers[i].Leave();
            }

            uiControllers.Clear();
        }

#endif

    }
}
