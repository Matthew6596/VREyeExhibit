using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEffect : MonoBehaviour
{
    public GameObject particlePrefab;
    public GameObject screenEffect;
    public float effectLifetime;
    public float afterEffectLifetime;

    private Renderer screenfxRender;
    private bool active = false;
    private float screenfxRate;
    private float screenAfterfxRate;
    private Color screenfxCol;

    // Start is called before the first frame update
    void Start()
    {
        screenfxRender =  screenEffect.GetComponent<Renderer>();
        screenfxRate = 1 / effectLifetime;
        screenAfterfxRate = -1 / afterEffectLifetime;
        screenfxCol = Color.white;
    }

    private void Update()
    {
        if (active && screenfxCol.a < 1) screenfxCol.a += screenfxRate*Time.deltaTime;
        else if (!active && screenfxCol.a > 0) screenfxCol.a += screenAfterfxRate*Time.deltaTime;

        screenfxRender.material.color = screenfxCol;
    }

    public void Activate()
    {
        StartCoroutine(doParticleEffects());
    }

    IEnumerator doParticleEffects()
    {
        GameObject particles = Instantiate(particlePrefab);
        screenEffect.SetActive(true);

        yield return new WaitForSeconds(effectLifetime);

        screenEffect.SetActive(false);
        Destroy(particles);
    }
}
