using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimation : MonoBehaviour
{
    private Animator anim;
    //public InputActionProperty gripInteraction;
    //public InputActionProperty triggerInteraction;


    private InputAction triggerAction,gripAction;
    private float gripVal, triggerVal;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        //---Set up input action---
        string hand = (gameObject.name.ToLower().Contains("left")) ? "LeftHand" : "RightHand";

        triggerAction = new("xr" + hand + "_triggerAnim", InputActionType.Value, "<XRController>{" + hand + "}/trigger", null, null, "Axis");
        triggerAction.performed += Trigger;
        triggerAction.started += Trigger;
        triggerAction.canceled += Trigger;
        triggerAction.Enable();

        gripAction = new("xr" + hand + "_gripAnim", InputActionType.Value, "<XRController>{" + hand + "}/grip", null, null, "Axis");
        gripAction.performed += Grip;
        gripAction.started += Grip;
        gripAction.canceled += Grip;
        gripAction.Enable();
        //---
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Grip", gripVal);
        anim.SetFloat("Trigger", triggerVal);
    }

    private void Trigger(InputAction.CallbackContext ctx)
    {
        triggerVal = ctx.ReadValue<float>();
    }
    private void Grip(InputAction.CallbackContext ctx)
    {
        gripVal = ctx.ReadValue<float>();
    }
}
