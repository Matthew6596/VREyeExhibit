using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public Transform targetTransform;
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.transform.CompareTag("Teleporter"))
        {
            GameObject p = other.gameObject;
            CharacterController cc = GetComponent<CharacterController>();
            cc.enabled = false;
            Vector3 offset = p.transform.position - transform.position;
            offset.y = 0;
            transform.position = targetTransform.position + offset;
            cc.enabled = true;
        }
    }
}
