using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCanvas : MonoBehaviour
{
    public Transform playerCamera, hand;
    public SpaceCanvas handUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool beOn = Quaternion.Angle(playerCamera.rotation, hand.rotation) < 55;
        if (handUI.on)
        {
            if (!beOn) handUI.TurnOff();
        }
        else
        {
            if (beOn) handUI.TurnOn();
        }
        
    }
}
