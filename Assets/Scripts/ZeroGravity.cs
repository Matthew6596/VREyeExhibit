using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGravity : MonoBehaviour
{
    [SerializeField]
    bool zeroGravityActive = false;
    Rigidbody[] rigidbodies;
    // Start is called before the first frame update
    void Start()
    {
        //Get all rigidbodies
        rigidbodies = FindObjectsOfType<Rigidbody>();
        //Make sure rigidbodies are/aren't using gravity
        UpdateRigidbodies();
    }

    public void ToggleZeroGravity()
    {
        zeroGravityActive = !zeroGravityActive;
        UpdateRigidbodies();
    }
    public void ToggleZeroGravity(bool on)
    {
        zeroGravityActive = on;
        UpdateRigidbodies();
    }
    private void UpdateRigidbodies()
    {
        foreach (Rigidbody r in rigidbodies)
        {
            //Set rigidbody's gravity on/off
            r.useGravity = !zeroGravityActive;

            //Add some random extra force (so things don't just stay still in zero grav, boring!)
            if(zeroGravityActive)
                r.AddForce(new Vector3(Random.Range(-.05f, .05f), Random.Range(-.05f, .05f), Random.Range(-.05f, .05f)),ForceMode.Impulse);
        }
    }
}
