using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportScript : MonoBehaviour
{
    public Vector3 manualOffset;
    public Transform targetTransform;
    public static void Teleport(CharacterController cc, TeleportationAnchor anchor)
    {
        Transform pt = cc.transform;
        cc.enabled = false;
        pt.position = anchor.teleportAnchorTransform.position;
        cc.enabled = true;
    }
    public void Teleport(TeleportationAnchor anchor)
    {
        Teleport(FindObjectOfType<CharacterController>(), anchor);
    }
    public void TeleportWDelay(TeleportPlayer tp)
    {
        tp.Teleport();
        TeleportEffect.Activate();
        StartCoroutine(delay(tp.provider.delayTime, () => { Teleport(FindObjectOfType<CharacterController>(), tp.anchor);}));
    }
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.transform.CompareTag("Teleporter"))
        {
            //GameObject p = other.gameObject;
            CharacterController cc = GetComponent<CharacterController>();
            cc.enabled = false;
            //Vector3 offset = p.transform.position - transform.position;
            //offset.y = 0;
            transform.position = targetTransform.position+manualOffset;
            cc.enabled = true;
        }
    }

    IEnumerator delay(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }
}
