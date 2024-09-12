using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCat : MonoBehaviour
{
    public GameObject particlePrefab;
    GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collect() //Invoked on grab?
    {
        //Teleport effect for cat
        StartCoroutine(catTeleport());
    }

    IEnumerator catTeleport()
    {
        //Begin teleport effect
        if (particlePrefab != null) particles = Instantiate(particlePrefab, transform);

        //Wait for teleport to end
        yield return new WaitForSeconds(1.5f);

        //End teleport effect
        if (particles != null) Destroy(particles);


        //Teleport cat to inside ship
    }
}
