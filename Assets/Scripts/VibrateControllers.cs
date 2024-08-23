using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VibrateControllers : MonoBehaviour
{
    private VibrateController[] vibrateControllers;

    private GameObject leftController;
    private GameObject rightController;
    private VibrateController vibrateLeftController;
    private VibrateController vibrateRightController;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }


    public void VibrateLeft()
    {
        leftController = GameObject.Find("Left Controller");
        vibrateLeftController = leftController.GetComponent<VibrateController>();
        vibrateLeftController.VibrateStrong(1000);
    }

    public void VibrateRight()
    {
        rightController = GameObject.Find("Right Controller");
        vibrateRightController = rightController.GetComponent<VibrateController>();
        vibrateRightController.VibrateStrong(1000);
    }
}
