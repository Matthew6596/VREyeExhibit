using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCat : MonoBehaviour
{
    //public GameObject particlePrefab;
    public int catsCollected;
    bool canCollect;
    Rigidbody rb;
    TeleportObjectScript teleportScript;
    CanvasTriggerArea canvasTriggerArea;
    //GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.useGravity = false;
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
            catsCollected++;
            canvasTriggerArea.enabled = false;
            StartCoroutine(ToggleGravity());
        }
    }

    IEnumerator ToggleGravity()
    {
        yield return new WaitForSeconds(1.5f);
        rb.useGravity = true;
    }
}
