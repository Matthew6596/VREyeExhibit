using System; //needed for Action
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportObjectScript : MonoBehaviour
{
    [Tooltip("The anchor the object is teleported to")]
    public TeleportationAnchor anchor = null;

    [Tooltip("Bool that controls whether the object can teleport")]
    public bool canTeleport = true;

    private XRGrabInteractable grabInteractable;

    // Start is called before the first frame update
    void Start()
    {
        anchor = GameObject.FindGameObjectWithTag("ObjectTeleporter").GetComponent<TeleportationAnchor>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    public void Teleport(TeleportationAnchor anchor) 
    {
        if (canTeleport)
        {
            transform.position = anchor.transform.position;
            canTeleport = false;
            grabInteractable.enabled = true; //Reenable interactable
        }
    }

    public void TeleportWDelay()
    {
        if(canTeleport)
        {
            //Teleport();
            TeleportEffectObject.Activate();
            StartCoroutine(delay(1.5f, () => { Teleport(anchor); }));
        }
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.transform.CompareTag("ObjectTeleporter") && canTeleport)
        {
            transform.position = anchor.transform.position;
        }
    }

    IEnumerator delay(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        grabInteractable.enabled = false; //Force player to drop it
        action();
    }
}
