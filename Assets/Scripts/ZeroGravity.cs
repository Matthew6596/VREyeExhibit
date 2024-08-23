using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGravity : MonoBehaviour
{
    public static bool zeroGravityActive = false;
    public static Rigidbody[] rigidbodies;
    // Start is called before the first frame update
    void Start()
    {
        RefreshRigidbodies();
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
                r.AddForce(new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f)), ForceMode.Impulse);
        }
    }
    public void RefreshRigidbodies()
    {
        //Get all rigidbodies
        rigidbodies = FindObjectsOfType<Rigidbody>();
        //Make sure rigidbodies are/aren't using gravity
        UpdateRigidbodies();
    }
    public static void AddRigidbody(Rigidbody rb)
    {
        List<Rigidbody> rbList = new(); rbList.AddRange(rigidbodies);
        rbList.Add(rb); rigidbodies = rbList.ToArray();
        rb.useGravity = !zeroGravityActive;
    }
    public static void TryAddObject(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null) AddRigidbody(rb);
    }
}
