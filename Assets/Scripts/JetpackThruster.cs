using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class JetpackThruster : MonoBehaviour
{
    //---Public Properties---
    public bool doRotation = false;
    public GameObject player;
    //public DynamicMoveProvider moveProvider;
    public float thrustPower;
    public GameObject particlesPrefab;

    //---Private Properties---
    private CharacterController playercc;
    private float thrustInput;
    private InputAction thrustAction;
    private Vector3 velocity;
    private Vector3 rotationVelocity;
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
        rotationVelocity=Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float rotationPower, movementPower;

        if (thrustInput > 0)
        {
            if (doRotation)
            {
                Vector3 a1 = transform.position - (player.transform.position + new Vector3(0, playercc.height / 2, 0));
                //dot -> 0 when perpendicular, -> -1 or 1 when parallel
                movementPower = Math.Abs(Vector3.Dot(a1.normalized, transform.forward));
                Debug.Log(movementPower);
                rotationPower = 1 - movementPower;
                velocity += thrustInput * movementPower * transform.forward;
                rotationVelocity += thrustInput * rotationPower * a1.magnitude * transform.forward;
            }
            else velocity += thrustInput * transform.forward;
        }
        playercc.Move(velocity * Time.deltaTime);
        player.transform.Rotate(rotationVelocity * Time.deltaTime);

    }

    public void Thrust(InputAction.CallbackContext ctx)
    {
        thrustInput = ctx.ReadValue<float>()*thrustPower;

        if (ctx.canceled) Destroy(particleInstance);
        else if (ctx.started) particleInstance = Instantiate(particlesPrefab, transform);
    }
}
