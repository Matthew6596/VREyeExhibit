using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
//using UnityEngine.XR.Interaction.Toolkit.

public class TeleportScript : MonoBehaviour
{
    public Transform targetTransform;
    CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        LocomotionSystem ls = GetComponent<LocomotionSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject p = collision.gameObject;
        CharacterController cc = p.GetComponent<CharacterController>();
        cc.enabled = false;
        Vector3 offset = transform.position-p.transform.position;
        p.transform.position = targetTransform.position+offset;
        cc.enabled = true;
    }
}
