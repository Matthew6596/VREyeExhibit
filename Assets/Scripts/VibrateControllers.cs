using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class VibrateControllers : MonoBehaviour
{
    private VibrateController[] vibrateControllers;
    private VibrateController[] leftVibrates= new VibrateController[0], rightVibrates= new VibrateController[0];

    private void Start()
    {
        StopVibrate(true);
        StopVibrate(false);
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

    private void GetVibrates(bool left)
    {
        string n = left ? "left" : "right";
        vibrateControllers = FindObjectsOfType<VibrateController>();
        List<VibrateController> v = new();
        for(int i=0; i<vibrateControllers.Length; i++)
        {
            if (vibrateControllers[i].gameObject.name.ToLower().Contains(n)) v.Add(vibrateControllers[i]);
        }
        if (left) leftVibrates = v.ToArray();
        else rightVibrates = v.ToArray();
    }
    public void Vibrate(bool left)
    {
        GetVibrates(left);
        foreach(VibrateController c in left ? leftVibrates : rightVibrates) c.VibrateStrong(10000);
    }
    public void StopVibrate(bool left)
    {
        GetVibrates(left);
        foreach (VibrateController c in left?leftVibrates:rightVibrates) c.VibrateWeak(0.001f);
    }
}
