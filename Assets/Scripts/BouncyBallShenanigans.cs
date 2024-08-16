using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBallShenanigans : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.AddForce(new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f)), ForceMode.Impulse);
    }
}
