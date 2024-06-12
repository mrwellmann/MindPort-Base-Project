using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils.Bindings;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GrabEnabler : MonoBehaviour
{
    public GameObject Go1; // GameObject to activate when grabbed by one hand
    public GameObject Go2; // GameObject to activate when grabbed by two hands

    private XRGrabInteractable grabInteractable;
    private int interactorCount = 0;
    private List<NearFarInteractor> nearFarInteractors;

    NearFarInteractor.Region m_selectionRegion;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Register event handlers for when the object is grabbed and released
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    void OnDestroy()
    {
        // Unregister event handlers to prevent memory leaks
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnNearFarSelectionRegionChanged(NearFarInteractor.Region selectionRegion)
    {
        m_selectionRegion = selectionRegion;

        if (selectionRegion == NearFarInteractor.Region.Near)
        {
            interactorCount++;
        }
        // else
        // {
        //     interactorCount--;
        //     if (interactorCount < 0)
        //     {
        //         interactorCount = 0;
        //     }
        // }

        if (interactorCount == 0)
        {
            Go1.SetActive(false);
            Go2.SetActive(false);
        }
        else if (interactorCount == 1)
        {
            Go1.SetActive(true);
            Go2.SetActive(false);
        }
        else if (interactorCount == 2)
        {
            Go1.SetActive(true);
            Go2.SetActive(true);
        }
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        var interactor = args.interactorObject as IXRInteractor;

        if (interactor is NearFarInteractor nearFarInteractor)
        {
            nearFarInteractor.selectionRegion.Subscribe(OnNearFarSelectionRegionChanged);
        };
    }

    void OnSelectExited(SelectExitEventArgs args)
    {
        var interactor = args.interactorObject as IXRInteractor;

        if (interactor is NearFarInteractor nearFarInteractor)
        {
            nearFarInteractor.selectionRegion.Unsubscribe(OnNearFarSelectionRegionChanged);
        };
    }

    private bool IsNearInteractor(IXRInteractor interactor)
    {
        if (interactor is NearFarInteractor nearFarInteractor)
        {
            return nearFarInteractor.selectionRegion.Value == NearFarInteractor.Region.Near;
        }
        return false;
    }
}