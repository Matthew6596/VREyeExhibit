using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public Transform targetTransform;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameObject p = collision.gameObject;
            CharacterController cc = p.GetComponent<CharacterController>();
            cc.enabled = false;
            Vector3 offset = transform.position - p.transform.position;
            p.transform.position = targetTransform.position + offset;
            cc.enabled = true;
        }
    }
}
