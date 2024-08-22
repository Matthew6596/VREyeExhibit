using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBallShenanigans : MonoBehaviour
{
    PlayQuickSound pqs;
    [SerializeField]
    bool stayStillAtStart = true;
    Rigidbody rb;
    bool randomBounce = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pqs = GetComponent<PlayQuickSound>();
        if (stayStillAtStart) StartCoroutine(startTimer());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (randomBounce)
        {
            rb.AddForce(new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f)), ForceMode.Impulse);
            pqs.Play();
        }
    }

    IEnumerator startTimer()
    {
        randomBounce = false;
        yield return new WaitForSeconds(1f);
        randomBounce = true;
    }
}
