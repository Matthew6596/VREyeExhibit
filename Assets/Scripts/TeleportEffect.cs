using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportEffect : MonoBehaviour
{
    public static TeleportEffect inst;

    public GameObject particlePrefab;
    public GameObject screenEffect;
    public GameObject flashEffect;
    public float effectLifetime;
    public float afterEffectLifetime;

    private Image screenfxRender;
    private Image flashfxImg;
    private bool _active = false;
    private float screenfxRate;
    private float screenAfterfxRate;
    private float flashfxRate;
    private Color screenfxCol;
    private Color flashfxCol;
    private bool flashActive = false;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        screenfxRender =  screenEffect.GetComponent<Image>();
        flashfxImg = flashEffect.GetComponent<Image>();
        screenfxRate = 1 / effectLifetime/2;
        screenAfterfxRate = -1 / afterEffectLifetime/2;
        flashfxRate = 1 / (effectLifetime / 2);
        screenfxCol = screenfxRender.color;
        flashfxCol = flashfxImg.color;
    }

    private void Update()
    {
        if (_active && screenfxCol.a < 1) screenfxCol.a += screenfxRate*Time.deltaTime;
        else if (!_active && screenfxCol.a > 0) screenfxCol.a += screenAfterfxRate*Time.deltaTime;
        if (flashActive && flashfxCol.a < 1) flashfxCol.a += flashfxRate * Time.deltaTime;
        else if (!flashActive && flashfxCol.a > 0) flashfxCol.a -= flashfxRate * Time.deltaTime;
        screenfxRender.color = screenfxCol;
        flashfxImg.color = flashfxCol;
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
        _active = true;

        yield return new WaitForSeconds(effectLifetime/2);
        flashActive = true;
        yield return new WaitForSeconds(effectLifetime/2);
        flashActive = false;
        _active = false;
        //screenEffect.SetActive(false);
        if(particles!=null) Destroy(particles);
    }
}
