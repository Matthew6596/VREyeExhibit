using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class XrCollider : MonoBehaviour
{
    CapsuleCollider col;
    CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.AddComponent<CapsuleCollider>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        col.height = cc.height+0.01f;
        col.radius = cc.radius + 0.01f;
        col.center = cc.center;
    }
}
