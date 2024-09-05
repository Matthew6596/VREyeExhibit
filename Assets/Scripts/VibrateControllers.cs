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

    private XRGrabInteractable simpleInteractable;

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

    private void GetLeft()
    {
        if (vibrateLeftController == null)
        {
            leftController = GameObject.Find("Left Controller");
            vibrateLeftController = leftController.GetComponent<VibrateController>();
        }
        if (vibrateLeftController2 == null)
        {
            leftController2 = GameObject.Find("Left Controller (1)");
            vibrateLeftController2 = leftController2.GetComponent<VibrateController>();
        }
    }
    private void GetRight()
    {
        if (vibrateRightController == null)
        {
            rightController = GameObject.Find("Right Controller");
            vibrateRightController = rightController.GetComponent<VibrateController>();
        }
        if (vibrateRightController2 == null)
        {
            rightController2 = GameObject.Find("Right Controller (1)");
            vibrateRightController2 = rightController2.GetComponent<VibrateController>();
        }
    }
    public void VibrateLeft()
    {
        GetLeft();
        if(vibrateLeftController!= null) vibrateLeftController.VibrateStrong(10000);
        if(vibrateLeftController2!=null) vibrateLeftController2.VibrateStrong(10000);
    }
    public void StopVibrateLeft()
    {
        GetLeft();
        if (vibrateLeftController != null) vibrateLeftController.VibrateWeak(0.001f);
        if(vibrateLeftController2!=null)vibrateLeftController2.VibrateWeak(0.001f);
    }
    public void StopVibrateRight()
    {
        GetRight();
        if (vibrateRightController != null) vibrateRightController.VibrateWeak(0.001f);
        if(vibrateRightController2!=null)vibrateRightController2.VibrateWeak(0.001f);
    }
    public void VibrateRight()
    {
        GetRight();
        if (vibrateRightController != null) vibrateRightController.VibrateStrong(10000);
        if(vibrateRightController2!=null)vibrateRightController2.VibrateStrong(10000);
    }
}
