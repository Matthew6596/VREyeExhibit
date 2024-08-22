using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSimpleInteractable))]
public class TriggerButton : MonoBehaviour
{
    public UnityEvent action;
    private XRSimpleInteractable simpleInteractable;
    bool hoveredLeft=false;
    bool hoveredRight=false;
    private void Start()
    {
        simpleInteractable = GetComponent<XRSimpleInteractable>();
        simpleInteractable.hoverEntered.AddListener((HoverEnterEventArgs a) => { HoveredOn(a); });
        simpleInteractable.hoverExited.AddListener((HoverExitEventArgs a) => { HoveredOff(a); });
    }
    public void HoveredOn(HoverEnterEventArgs a) 
    {
        string controllerName = a.interactorObject.transform.parent.name;
        Debug.Log(controllerName+" hovered button");
        if (controllerName.ToLower().Contains("left")) hoveredLeft = true;
        else hoveredRight = true;
    }
    public void HoveredOff(HoverExitEventArgs a) 
    {
        string controllerName = a.interactorObject.transform.parent.name;
        if (controllerName.ToLower().Contains("left")) hoveredLeft = false;
        else hoveredRight = false;
    }
    public void PerformAction(bool leftController)
    {
        if ((leftController&&hoveredLeft)||(!leftController && hoveredRight)) action.Invoke();
    }
}
