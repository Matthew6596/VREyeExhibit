using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform player;
    private BoxCollider c;

    private int q=100;
    public int queue { get => q; set { q = value; setAlpha(); } }

    PlayQuickSound pqs;

    public Action enterAction;

    // Start is called before the first frame update
    void Start()
    {
        pqs = GetComponent<PlayQuickSound>();
        c = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!c.bounds.Contains(player.position)) return;

        if (queue == 0) //yay!
        {
            if(pqs!=null)pqs.Play();
            queue = -1;
            enterAction();
        }
        else if(queue!=-1)//Entered wrong checkpoint, too early!
        {

        }
    }

    private void setAlpha()
    {
        Material mat = GetComponent<Renderer>().material;
        Color b = mat.color;
        switch (q)
        {
            case 0: b.a=1; break;
            case 1: b.a=.5f; break;
            case 2: b.a=.25f; break;
            default: b.a = 0; break;
        }
        mat.color = b;
    }
}
