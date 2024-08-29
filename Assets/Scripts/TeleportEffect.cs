using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEffect : MonoBehaviour
{
    public GameObject particlePrefab;
    public GameObject screenEffect;
    public float effectLifetime;

    private Renderer screenfxRender;
    private bool active = false;
    private float screenfxRate;
    private Color screenfxCol;

    // Start is called before the first frame update
    void Start()
    {
        screenfxRender =  screenEffect.GetComponent<Renderer>();
        screenfxRate = 1 / effectLifetime;
        screenfxCol = Color.white;
    }

    private void Update()
    {
        if (!active) return;

        screenfxRender.material.color = screenfxCol;
        screenfxCol.a += screenfxRate;
    }

    public void Activate()
    {
        StartCoroutine(doParticleEffects());
    }

    IEnumerator doParticleEffects()
    {
        GameObject particles = Instantiate(particlePrefab);
        screenEffect.SetActive(true);
        screenfxCol.a = 0;
        screenfxRender.material.color = screenfxCol;

        yield return new WaitForSeconds(effectLifetime);

        screenEffect.SetActive(false);
        Destroy(particles);
    }
}
