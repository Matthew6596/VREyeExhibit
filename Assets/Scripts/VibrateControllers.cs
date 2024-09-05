using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class VibrateControllers : MonoBehaviour
{
    //private VibrateController[] vibrateControllers;

    private GameObject leftController, rightController, leftController2, rightController2;
    private VibrateController vibrateLeftController, vibrateRightController, vibrateLeftController2, vibrateRightController2;

    //public UnityEvent action;
    private XRGrabInteractable simpleInteractable;
    //bool hoveredLeft = false;
    //bool hoveredRight = false;
    private void Start()
    {
        simpleInteractable = GetComponent<XRGrabInteractable>();
        simpleInteractable.selectEntered.AddListener((SelectEnterEventArgs a) => { GrabOn(a); });
        simpleInteractable.selectExited.AddListener((SelectExitEventArgs a) => { GrabOff(a); });
    }
    public void GrabOn(SelectEnterEventArgs a)
    {
        string controllerName = a.interactorObject.transform.parent.name;
        Debug.Log(controllerName);
        if (controllerName.ToLower().Contains("left")) VibrateLeft();
        if (controllerName.ToLower().Contains("right")) VibrateRight();
    }
    public void GrabOff(SelectExitEventArgs a)
    {
        string controllerName = a.interactorObject.transform.parent.name;
        if (controllerName.ToLower().Contains("left")) StopVibrateLeft();
        if (controllerName.ToLower().Contains("right")) StopVibrateRight();
    }
    /*public void PerformAction(bool leftController)
    {
        if ((leftController && hoveredLeft)) VibrateLeft();
        else if (!leftController && hoveredRight) VibrateRight();
    }
    public void UnPerformAction(bool leftController)
    {
        StopVibrateLeft();
        StopVibrateRight();
    }*/

    private void GetLeft()
    {
        if (vibrateLeftController == null)
        {
            leftController = GameObject.Find("Left Controller");
            leftController2 = GameObject.Find("Left Controller (1)");
            vibrateLeftController = leftController.GetComponent<VibrateController>();
            vibrateLeftController2 = leftController2.GetComponent<VibrateController>();
        }
    }
    private void GetRight()
    {
        if (vibrateRightController == null)
        {
            rightController = GameObject.Find("Right Controller");
            rightController2 = GameObject.Find("Right Controller (1)");
            vibrateRightController = rightController.GetComponent<VibrateController>();
            vibrateRightController2 = rightController2.GetComponent<VibrateController>();
        }
    }
    public void VibrateLeft()
    {
        GetLeft();
        vibrateLeftController.VibrateStrong(10000);
        vibrateLeftController2.VibrateStrong(10000);
    }
    public void StopVibrateLeft()
    {
        GetLeft();
        vibrateLeftController.VibrateWeak(0.001f);
        vibrateLeftController2.VibrateWeak(0.001f);
    }
    public void StopVibrateRight()
    {
        GetRight();
        vibrateRightController.VibrateWeak(0.001f);
        vibrateRightController2.VibrateWeak(0.001f);
    }
    public void VibrateRight()
    {
        GetRight();
        vibrateRightController.VibrateStrong(10000);
        vibrateRightController2.VibrateStrong(10000);
    }
}
