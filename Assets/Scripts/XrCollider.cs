using UnityEngine;

public class XrCollider : MonoBehaviour
{
    [Tooltip("The scale difference from the Capsule Collider to the Character Controller")]
    public float extraScale = 0.05f;
    CapsuleCollider col;
    CharacterController cc;
    public bool isTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.AddComponent<CapsuleCollider>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        col.height = cc.height+extraScale;
        col.radius = cc.radius +extraScale;
        col.center = cc.center;
        col.isTrigger = isTrigger;
    }
}
