using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTriggerArea : MonoBehaviour
{
    public SpaceCanvas canvas;
    public bool turnOn = false;

    PlayQuickSound pqs;

    private void Start()
    {
        pqs = GetComponent<PlayQuickSound>();
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (turnOn) canvas.TurnOn();
        else canvas.TurnOff();
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (turnOn && !canvas.on) { canvas.TurnOn(); pqs.Play(); }
            else if (!turnOn && canvas.on) canvas.TurnOff();
        }
    }
    /*private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (turnOn && !canvas.on) canvas.TurnOn();
        else canvas.TurnOff();
    }*/
}
