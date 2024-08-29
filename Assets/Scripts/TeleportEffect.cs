using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportEffect : MonoBehaviour
{
    public static TeleportEffect inst;

    public GameObject particlePrefab;
    public GameObject screenEffect;
    public float effectLifetime;
    public float afterEffectLifetime;

    private Image screenfxRender;
    private bool _active = false;
    private float screenfxRate;
    private float screenAfterfxRate;
    private Color screenfxCol;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        screenfxRender =  screenEffect.GetComponent<Image>();
        screenfxRate = 1 / effectLifetime;
        screenAfterfxRate = -1 / afterEffectLifetime;
        screenfxCol = screenfxRender.color;
    }

    private void Update()
    {
        if (_active && screenfxCol.a < 1) screenfxCol.a += screenfxRate*Time.deltaTime;
        else if (!_active && screenfxCol.a > 0) screenfxCol.a += screenAfterfxRate*Time.deltaTime;

        screenfxRender.color = screenfxCol;
    }

    public void activate()
    {
        StartCoroutine(doParticleEffects());
    }
    public static void Activate() { inst.activate(); }

    IEnumerator doParticleEffects()
    {
        GameObject particles=null;
        if(particlePrefab!=null) particles = Instantiate(particlePrefab);
        //screenEffect.SetActive(true);

        yield return new WaitForSeconds(effectLifetime);

        //screenEffect.SetActive(false);
        if(particles!=null) Destroy(particles);
    }
}
