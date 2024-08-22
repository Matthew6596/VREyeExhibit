using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SpaceCanvas : MonoBehaviour
{
    //---Public Properties---
    public bool hasSubMenu=false;
    public GameObject[] summonPrefabs;
    public Transform summonSpawn;
    public TMP_Dropdown summonDropdown;

    //

    //---Private Properties---
    Transform bg;
    Transform submenu;
    Vector3 baseBGScale;
    Vector3 targetBGScale;
    bool on = false;
    List<Coroutine> delayCoroutines=new();
    float smallScale = 0.05f;
    Action bufferedAction;
    Transform showTextBoxArea;
    TMP_Text showText;
    
    //

    //---Start Method---
    void Start()
    {
        bg=transform.GetChild(0);
        if (hasSubMenu)
        {
            submenu = bg.GetChild(1);
            showTextBoxArea = submenu.GetChild(0);
            showText = showTextBoxArea.GetChild(0).GetComponent<TMP_Text>();
            for (int i = 0; i < submenu.childCount; i++) submenu.GetChild(i).gameObject.SetActive(false);
            submenu.gameObject.SetActive(false);
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
        bg.localScale = new Vector3(targetBGScale.x, bg.localScale.y, 1);
        targetBGScale = new Vector3(baseBGScale.x,baseBGScale.y*smallScale,1);
        Delay(1f, () => { targetBGScale = baseBGScale; });
    }
    public void TurnOff()
    {
        targetBGScale = new Vector3(baseBGScale.x, baseBGScale.y*smallScale, 1);
        Delay(1f, () => { targetBGScale = Vector3.zero; });  
    }
    public void ResetWorld()
    {
        CloseSubMenu(true);
        bufferedAction = () => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); };
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
