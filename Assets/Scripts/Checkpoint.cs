using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform player;
    private BoxCollider[] cs;

    private int q=100;
    public int queue { get => q; set { q = value; setAlpha(); } }

    PlayQuickSound pqs;

    public Action enterAction;

    // Start is called before the first frame update
    void Start()
    {
        pqs = GetComponent<PlayQuickSound>();
        cs = GetComponents<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        bool playerin = false;
        for(int i=0; i<cs.Length; i++)
            if (cs[i].bounds.Contains(player.position)) { playerin = true; break; }
        if (!playerin) return;


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
        b.a = q switch
        {
            0 => 1, 1 => .5f, 2 => .25f, _ => 0,
        };
        mat.color = b;
    }
}
