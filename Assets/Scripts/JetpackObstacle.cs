using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackObstacle : MonoBehaviour
{
    JetpackThruster[] thrusters;
    public float stopForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("collision");
            thrusters = FindObjectsOfType<JetpackThruster>();
            foreach(JetpackThruster jet in thrusters) jet.MultiplyVelocity(stopForce);
        }
    }
}
