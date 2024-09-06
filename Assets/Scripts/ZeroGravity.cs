using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ZeroGravity : MonoBehaviour
{
    public static ZeroGravity inst;

    public static bool zeroGravityActive = false;
    public static Rigidbody[] rigidbodies;
    //public static JetpackThruster[] thrusters;
    public static DynamicMoveProvider moveProvider;
    //private static float pMoveSpeed;

    
    public GameObject leftHand, rightHand, leftThruster, rightThruster;
    public bool zeroGravityStartOn = false;
    public AudioSource spaceshipAmbient;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        moveProvider = FindObjectOfType<DynamicMoveProvider>();
        RefreshRigidbodies();
        StartCoroutine(checkControllers());
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

    /*IEnumerator tryGetThrusters()
    {
        while (thrusters == null || thrusters.Length < 2)
        {
            thrusters = FindObjectsOfType<JetpackThruster>();
            yield return null;
        }
        ToggleZeroGravity(zeroGravityStartOn);
    }*/

    IEnumerator checkControllers()
    {
        CheckControllerState();
        yield return new WaitForSeconds(1);
        StartCoroutine(checkControllers());
    }

    public void TogglePlayerGravity(bool on)
    {
        zeroGravityActive = on;
        CheckControllerState();
    }
    public void CheckControllerState()
    {
        //while (thrusters == null || thrusters.Length < 2) thrusters = FindObjectsOfType<JetpackThruster>();
        if (zeroGravityActive)
        {
            //thrusters[0].enabled = true;
            //thrusters[1].enabled = true;
            leftThruster.SetActive(true);
            rightThruster.SetActive(true);
            leftHand.SetActive(false);
            rightHand.SetActive(false);
            moveProvider.useGravity = false;
            moveProvider.moveSpeed = 0;
            //if(spaceshipAmbient.isPlaying) spaceshipAmbient.Stop();
        }
        else
        {
            //thrusters[0].enabled = false;
            //thrusters[1].enabled = false;
            leftThruster.SetActive(false);
            rightThruster.SetActive(false);
            leftHand.SetActive(true);
            rightHand.SetActive(true);
            moveProvider.useGravity = true;
            moveProvider.moveSpeed = 1;
            //if (!spaceshipAmbient.isPlaying) spaceshipAmbient.Play();
        }
    }

    public void ToggleSpaceshipAmbient(bool on)
    {
        StartCoroutine(soundDelay(on));
    }
    public static void ToggleSpaceshipAmbientS(bool on)
    {
        if (on) inst.spaceshipAmbient.Play();
        else inst.spaceshipAmbient.Stop();
    }

    public void TogglePlayerGravityWDelay(bool on)
    {
        StartCoroutine(delay(on));
    }

    IEnumerator delay(bool on)
    {
        yield return new WaitForSeconds(1.5f);
        TogglePlayerGravity(on);
    }
    IEnumerator soundDelay(bool on)
    {
        yield return new WaitForSeconds(1.5f);
        ToggleSpaceshipAmbientS(on);
    }
}
