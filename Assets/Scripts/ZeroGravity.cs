using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ZeroGravity : MonoBehaviour
{
    public static bool zeroGravityActive = false;
    public static Rigidbody[] rigidbodies;
    public static JetpackThruster[] thrusters;
    public static DynamicMoveProvider moveProvider;
    private static float pMoveSpeed;

    public bool zeroGravityStartOn = false;

    // Start is called before the first frame update
    void Start()
    {
        moveProvider = FindObjectOfType<DynamicMoveProvider>();
        pMoveSpeed = moveProvider.moveSpeed;
        RefreshRigidbodies();
        StartCoroutine(tryGetThrusters());
    }

    public void ToggleZeroGravity(){ToggleZeroGravity(!zeroGravityActive);}
    public void ToggleZeroGravity(bool on)
    {
        TogglePlayerGravity(on);
        UpdateRigidbodies();
    }
    private void UpdateRigidbodies()
    {
        foreach (Rigidbody r in rigidbodies)
        {
            if (r.gameObject.GetComponent<CanvasTriggerArea>() == null)
            {
                //Set rigidbody's gravity on/off
                r.useGravity = !zeroGravityActive;

                //Add some random extra force (so things don't just stay still in zero grav, boring!)
                if (zeroGravityActive)
                    r.AddForce(new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f)), ForceMode.Impulse);
            }
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

    IEnumerator tryGetThrusters()
    {
        while (thrusters == null || thrusters.Length < 2)
        {
            thrusters = FindObjectsOfType<JetpackThruster>();
            yield return null;
        }
        ToggleZeroGravity(zeroGravityStartOn);
    }

    public void TogglePlayerGravity(bool on)
    {
        zeroGravityActive = on;
        if (on)
        {
            thrusters[0].enabled = true;
            thrusters[1].enabled = true;
            moveProvider.useGravity = false;
            pMoveSpeed = moveProvider.moveSpeed;
            moveProvider.moveSpeed = 0;
        }
        else
        {
            thrusters[0].enabled = false;
            thrusters[1].enabled = false;
            //moveProvider.enabled = true;
            moveProvider.useGravity = true;
            moveProvider.moveSpeed = pMoveSpeed;
        }
    }
}
