using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private int q=100;
    public int queue { get => q; set { q = value; setAlpha(); } }

    PlayQuickSound pqs;

    public Action enterAction;

    // Start is called before the first frame update
    void Start()
    {
        pqs = GetComponent<PlayQuickSound>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setAlpha()
    {
        Material mat = GetComponent<Renderer>().material;
        Color b = mat.color;
        switch (q)
        {
            case 0: b.a=1; break;
            case 1: b.a=.67f; break;
            case 2: b.a=.33f; break;
            default: b.a = 0; break;
        }
        mat.color = b;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (queue == 0) //yay!
            {
                pqs.Play();
                enterAction();
            }
            else //Entered wrong checkpoint, too early!
            {

            }
        }
    }
}
