using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JetpackThruster : MonoBehaviour
{
    //---Public Properties---
    public bool doRotation = false;
    public GameObject player;
    public float thrustPower;
    public GameObject particlesPrefab;
    public float brakeSensitivity = 4;

    //---Private Properties---
    private CharacterController playercc;
    private float thrustInput;
    private InputAction thrustAction;
    private InputAction brakeAction;
    private Vector3 velocity;
    private Vector3 rotationVelocity;
    private GameObject particleInstance;
    private float brakeAmt=1;
    private VibrateController vibrate;
    private AudioSource _audio;

    private void Awake()
    {
        //---Set up input action---
        string hand = (gameObject.name.ToLower().Contains("left")) ? "LeftHand" : "RightHand";

        thrustAction = new("xr"+hand+"thrust",InputActionType.Value, "<XRController>{" + hand + "}/trigger",null,null,"Axis");
        thrustAction.performed += Thrust;
        thrustAction.started += Thrust;
        thrustAction.canceled += Thrust;
        thrustAction.Enable();

        brakeAction = new("xr" + hand + "brake", InputActionType.Value, "<XRController>{" + hand + "}/grip", null, null, "Axis");
        brakeAction.performed += Brake;
        brakeAction.started += Brake;
        brakeAction.canceled += Brake;
        brakeAction.Enable();
        //---
    }

    // Start is called before the first frame update
    void Start()
    {
        playercc = player.GetComponent<CharacterController>();
        vibrate = GetComponent<VibrateController>();
        //StartCoroutine(trackVelocity());
    }

    // Update is called once per frame
    void Update()
    {
        float rotationPower, movementPower;

        if (thrustInput > 0)
        {
            if(!_audio.isPlaying) _audio.Play();
            vibrate.VibrateWeak(Time.deltaTime);

            if (doRotation)
            {
                Vector3 a1 = transform.position - (player.transform.position + playercc.center);
                //dot -> 0 when perpendicular, -> -1 or 1 when parallel
                movementPower = Math.Abs(Vector3.Dot(a1.normalized, transform.forward));
                Debug.Log(movementPower);
                rotationPower = 1 - movementPower;
                velocity += thrustInput * movementPower * transform.forward;
                rotationVelocity += thrustInput * rotationPower * a1.magnitude * transform.forward;
            }
            else velocity += thrustInput * transform.forward;
        }
        else
        {
            if(_audio.isPlaying) _audio.Stop();
        }

        velocity *= brakeAmt;
        playercc.Move(velocity * Time.deltaTime);
        player.transform.Rotate(rotationVelocity * Time.deltaTime);
    }

    public void Thrust(InputAction.CallbackContext ctx)
    {
        if (!ZeroGravity.zeroGravityActive) return;

        thrustInput = ctx.ReadValue<float>()*thrustPower;

        if (ctx.canceled) Destroy(particleInstance);
        else if (ctx.started) particleInstance = Instantiate(particlesPrefab, transform);
    }
    public void Brake(InputAction.CallbackContext ctx)
    {
        if (!ZeroGravity.zeroGravityActive) return;

        brakeAmt = 1-ctx.ReadValue<float>()/brakeSensitivity;
        
    }

    public void StopVelocity() { velocity = Vector3.zero; }

    IEnumerator trackVelocity()
    {
        Vector3 p1 = player.transform.position;
        yield return new WaitForSeconds(0.1f);
        Vector3 trackedVelocity = player.transform.position - p1;
        if (Mathf.Approximately(trackedVelocity.x, 0)) velocity.x = 0;
        //else if(Mathf.Abs(trackedVelocity.x - velocity.x) > 1) velocity.x = trackedVelocity.x;
        if (Mathf.Approximately(trackedVelocity.y, 0)) velocity.y = 0;
        //else if (Mathf.Abs(trackedVelocity.y - velocity.y) > 1) velocity.y = trackedVelocity.y;
        if (Mathf.Approximately(trackedVelocity.z, 0)) velocity.z = 0;
        //else if (Mathf.Abs(trackedVelocity.z - velocity.z) > 1) velocity.z = trackedVelocity.z;
        StartCoroutine(trackVelocity());
    }
}
