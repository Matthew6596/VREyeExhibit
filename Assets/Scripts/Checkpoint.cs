using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform player;

    private int q=100;
    public int queue { get => q; set { q = value; setAlpha(); } }

    PlayQuickSound pqs;

    public Action enterAction;

    private Vector3 playerPrevPos;
    private Plane intersectPlane;
    public float radius=1;

    // Start is called before the first frame update
    void Start()
    {
        pqs = GetComponent<PlayQuickSound>();
        playerPrevPos = transform.position;
        intersectPlane = new Plane(transform.up, transform.position);
        playerSide = intersectPlane.GetSide(player.position);
    }

    // Update is called once per frame
    bool playerSide;
    void Update()
    {
        //---New ring checkpoint collision/trigger logic---

        Ray r = new(playerPrevPos, player.position - playerPrevPos); //player movement ray
        bool s = intersectPlane.GetSide(player.position); //which side of plane is player on?
        if (s!=playerSide) //Player switched from one side of checkpoint to other
        {
            playerSide = s;
            if (intersectPlane.Raycast(r, out float e)) //Player's movement raycast intersects plane
            {
                if (Vector3.Distance(transform.position, r.GetPoint(e)) <= radius) //intersects inside ring radius
                {
                    if (queue == 0) //yay, player entered checkpoint!
                    {
                        if (pqs != null) pqs.Play();
                        queue = -1;
                        enterAction();
                    }
                    else if (queue != -1)//This is the wrong checkpoint, too early!
                    {

                    }
                }
            }
        }

        playerPrevPos = player.position;
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
