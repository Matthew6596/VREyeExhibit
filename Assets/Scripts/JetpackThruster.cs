using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class JetpackThruster : MonoBehaviour
{
    //---Public Properties---
    public GameObject player;
    //public DynamicMoveProvider moveProvider;
    public float thrustPower;
    public GameObject particlesPrefab;

    //---Private Properties---
    private CharacterController playercc;
    private float thrustInput;
    private InputAction thrustAction;
    private Vector3 velocity;
    private GameObject particleInstance;

    private void Awake()
    {
        //---Set up input action---
        string hand = (gameObject.name.ToLower().Contains("left")) ? "LeftHand" : "RightHand";
        thrustAction = new("xr"+hand+"thrust",InputActionType.Value, "<XRController>{" + hand + "}/trigger",null,null,"Axis");
        thrustAction.performed += Thrust;
        thrustAction.started += Thrust;
        thrustAction.canceled += Thrust;
        thrustAction.Enable();
        //---
    }

    // Start is called before the first frame update
    void Start()
    {
        playercc = player.GetComponent<CharacterController>();
        velocity= Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        velocity += thrustInput * transform.forward;
        playercc.Move(velocity * Time.deltaTime);
    }

    public void Thrust(InputAction.CallbackContext ctx)
    {
        thrustInput = ctx.ReadValue<float>()*thrustPower;

        if (ctx.canceled) Destroy(particleInstance);
        else if (ctx.started) particleInstance = Instantiate(particlesPrefab, transform);
    }
}
