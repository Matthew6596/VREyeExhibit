using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportEffectObject : MonoBehaviour
{
    public static TeleportEffectObject inst;

    public GameObject particlePrefab;
    public float effectLifetime;
    public float afterEffectLifetime;

    private bool _active = false;
    private Transform objectTransform;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        objectTransform = transform; //??
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activate()
    {
        StartCoroutine(doParticleEffects());
    }
    public static void Activate() { inst.activate(); }

    IEnumerator doParticleEffects()
    {
        GameObject particles = null;
        if (particlePrefab != null) particles = Instantiate(particlePrefab, objectTransform);
        _active = true;
        yield return new WaitForSeconds(effectLifetime);
        _active = false;
        if (particles != null) Destroy(particles);
    }
}
