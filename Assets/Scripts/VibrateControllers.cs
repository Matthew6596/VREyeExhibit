using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class VibrateControllers : MonoBehaviour
{
    private VibrateController[] vibrateControllers;

    private VibrateController[] leftVibrates, rightVibrates;

    //private XRGrabInteractable simpleInteractable;

    private void Start()
    {
        //simpleInteractable = GetComponent<XRGrabInteractable>();
        //simpleInteractable.selectEntered.AddListener((SelectEnterEventArgs a) => { GrabOn(a); });
        //simpleInteractable.selectExited.AddListener((SelectExitEventArgs a) => { GrabOff(a); });
    }
    public void GrabOn(SelectEnterEventArgs a)
    {
        string controllerName = a.interactorObject.transform.parent.name.ToLower();
        Debug.Log(controllerName);
        if (controllerName.Contains("left")) Vibrate(true);
        if (controllerName.Contains("right")) Vibrate(false);
    }
    public void GrabOff(SelectExitEventArgs a)
    {
        string controllerName = a.interactorObject.transform.parent.name.ToLower();
        if (controllerName.Contains("left")) StopVibrate(true);
        if (controllerName.Contains("right")) StopVibrate(false);
    }

    private void GetLeft()
    {
        vibrateControllers = FindObjectsOfType<VibrateController>();
        List<VibrateController> lefts = new List<VibrateController>();
        for(int i=0; i<vibrateControllers.Length; i++)
        {
            if (vibrateControllers[i].gameObject.name.Contains("Left")) lefts.Add(vibrateControllers[i]);
        }
        leftVibrates = lefts.ToArray();

    }
    private void GetRight()
    {
        vibrateControllers = FindObjectsOfType<VibrateController>();
        List<VibrateController> rights = new List<VibrateController>();
        for (int i = 0; i < vibrateControllers.Length; i++)
        {
            if (vibrateControllers[i].gameObject.name.Contains("Right")) rights.Add(vibrateControllers[i]);
        }
        leftVibrates = rights.ToArray();
    }
    public void Vibrate(bool left)
    {
        GetLeft();
        foreach(VibrateController c in left ? leftVibrates : rightVibrates) c.VibrateStrong(10000);
    }
    public void StopVibrate(bool left)
    {
        GetLeft();
        foreach (VibrateController c in left?leftVibrates:rightVibrates) c.VibrateWeak(0.001f);
    }
}
