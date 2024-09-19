using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCat : MonoBehaviour
{
    public CourseScript courseScript;
    bool canCollect;
    Rigidbody rb;
    TeleportObjectScript teleportScript;
    CanvasTriggerArea canvasTriggerArea;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        teleportScript = GetComponent<TeleportObjectScript>();
        canvasTriggerArea = GetComponent<CanvasTriggerArea>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collect() 
    {
        canCollect = teleportScript.TeleportWDelay();
        if(canCollect)
        {
            canvasTriggerArea.enabled = false;
            StartCoroutine(ToggleGravity());
            courseScript.CollectCat(gameObject);
        }
    }

    IEnumerator ToggleGravity()
    {
        yield return new WaitForSeconds(1.5f);
        rb.useGravity = true;
    }
}
