using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class SpaceCanvas : MonoBehaviour
{
    //---Public Properties---
    public bool hasSubMenu=false;
    [Space(10)]
    public GameObject[] summonPrefabs;
    public Transform summonSpawn;
    public TMP_Dropdown summonDropdown;
    public TeleportationAnchor[] teleportAnchors;
    public TMP_Dropdown teleportDropdown;
    public SpaceCanvas outsideCanvas;


    //

    //---Private Properties---
    Transform bg;
    Transform submenu;
    Vector3 baseBGScale;
    Vector3 targetBGScale;
    public bool on = false;
    List<Coroutine> delayCoroutines=new();
    float smallScale = 0.05f;
    Action bufferedAction;
    Transform showTextBoxArea;
    TMP_Text showText;
    TeleportPlayer teleportPlayer;
    CharacterController cc;
    
    //

    //---Start Method---
    void Start()
    {
        bg=transform.GetChild(0);
        cc = FindObjectOfType<CharacterController>();
        if (hasSubMenu)
        {
            submenu = bg.GetChild(1);
            showTextBoxArea = submenu.GetChild(0);
            showText = showTextBoxArea.GetChild(0).GetComponent<TMP_Text>();
            for (int i = 0; i < submenu.childCount; i++) submenu.GetChild(i).gameObject.SetActive(false);
            submenu.gameObject.SetActive(false);
            teleportPlayer = GetComponent<TeleportPlayer>();
        }
        baseBGScale = bg.localScale;
        bg.transform.localScale = Vector3.zero;
    }
    //---

    //---Update Method---
    void Update()
    {
        TweenBGScale(6f);
    }
    //---

    //---Public Methods---
    public void Toggle()
    {
        ClearDelays();
        on=!on;
        if (on) TurnOn();
        else TurnOff();
    }
    public void TurnOn()
    {
        on = true;
        bg.localScale = new Vector3(targetBGScale.x, bg.localScale.y, 1);
        targetBGScale = new Vector3(baseBGScale.x,baseBGScale.y*smallScale,1);
        Delay(1f, () => { targetBGScale = baseBGScale; });
    }
    public void TurnOff()
    {
        on = false;
        targetBGScale = new Vector3(baseBGScale.x, baseBGScale.y*smallScale, 1);
        Delay(1f, () => { targetBGScale = Vector3.zero; });  
    }
    public void ResetWorld()
    {
        CloseSubMenu(true);
        bufferedAction = () => {
            ZeroGravity.zeroGravityActive = false;
            //ZeroGravity.rigidbodies = new Rigidbody[];
            TargetScript.score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        };
    }
    public void ConfirmAction(bool confirmed)
    {
        if (confirmed) bufferedAction();
        else CloseSubMenu(false);
    }
    public void CloseSubMenu(bool subMenuOn)
    {
        for (int i = 0; i < submenu.childCount; i++) submenu.GetChild(i).gameObject.SetActive(false);
        submenu.gameObject.SetActive(subMenuOn);
    }
    public void SummonBtn()
    {
        CloseSubMenu(true);
        bufferedAction = () => 
        { 
            ZeroGravity.TryAddObject(Instantiate(summonPrefabs[summonDropdown.value], summonSpawn));
        };
    }
    public void TeleportBtn()
    {
        CloseSubMenu(true);
        bufferedAction = () =>
        {
            teleportPlayer.anchor = teleportAnchors[teleportDropdown.value];
            //Debug.Log(teleportPlayer.anchor.teleportAnchorTransform.position);
            teleportPlayer.Teleport();
            TeleportScript.Teleport(cc, teleportPlayer.anchor);
            ZeroGravity.inst.TogglePlayerGravity(true);
            /*Delay(1, () =>
            {
                //cc.enabled = false;
                //teleportPlayer.Teleport();
                //cc.enabled = true;
                ZeroGravity.inst.TogglePlayerGravity(true);
                
            });*/
            outsideCanvas.TurnOn();
        };
    }
    public void SetShowText(string txt)
    {
        showText.text = txt;
        showTextBoxArea.gameObject.SetActive(true);
    }
    //---

    //---Delaying actions---
    private void Delay(float seconds, Action action)
    {
        delayCoroutines.Add(StartCoroutine(timerCoroutine(seconds, action)));
    }
    IEnumerator timerCoroutine(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }
    public void TweenBGScale(float rate)
    {
        bg.transform.localScale += rate * Time.deltaTime * (targetBGScale-bg.transform.localScale);
    }
    public void ClearDelays()
    {
        foreach(Coroutine c in delayCoroutines) StopCoroutine(c);
    }
    //---
}
