using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using System;

public class CourseScript : MonoBehaviour
{
    //---Public serialized fields---
    public Checkpoint[] checkpoints;
    public GameObject[] cats;

    public TMP_Text mainTimeText,statsTxt;
    //---

    //---Private fields---
    static int totalCats = 10; //CHANGE LATER
    int catsCollected=0,totalCatsCollected=0;

    //Timer stuff
    private Stopwatch timer = new();
    private Stopwatch lapTimer = new();
    float[] laps; //in seconds
    float finalTime=float.NaN, bestTime=float.NaN;
    bool txtOverride = false;

    //Checkpoint stuff
    Checkpoint target;
    int targetIndex = 0;

    //Cat stuff
    int catIndex;

    //---

    // Start is called before the first frame update
    void Start()
    {
        foreach (Checkpoint cp in checkpoints) cp.enterAction += NextCheckpoint;
        laps = new float[checkpoints.Length];
        DisplayStatText();
    }

    public void Begin()
    {
        targetIndex = 0;
        target = checkpoints[0];
        timer = Stopwatch.StartNew();
        lapTimer = Stopwatch.StartNew();
        mainTimeText.gameObject.SetActive(true);
        mainTimeText.text = "0.000";
        //Set checkpoint queues (so checkpoints know when they are next)
        for (int i = targetIndex, cnt = 0; i < checkpoints.Length; i++, cnt++) checkpoints[i].queue = cnt;
    }

    void NextCheckpoint() //Called by checkpoint when entered
    {
        //Get the lap's time
        laps[targetIndex] = (float)lapTimer.Elapsed.TotalSeconds;
        lapTimer.Restart();

        //Show the lap splits
        txtOverride = true;
        mainTimeText.text = "Checkpoint " + (targetIndex + 1) + ": " + TimeToText(laps[targetIndex]);
        DelayAction(() => { txtOverride = false; }, 1.5f);

        //Increase lap count, check for completion, get next checkpoint
        targetIndex++;
        if (targetIndex == checkpoints.Length) { Complete(); return; }
        target = checkpoints[targetIndex];

        //Set checkpoint queues (so checkpoints know when they are next)
        for (int i = targetIndex, cnt = 0; i < checkpoints.Length; i++, cnt++) checkpoints[i].queue = cnt;
    }

    void Complete() //Win obstacle course
    {
        timer.Stop();
        finalTime = (float)timer.Elapsed.TotalSeconds;
        if(finalTime<bestTime || float.IsNaN(bestTime))bestTime = finalTime;
        checkpoints[^1].queue = -1;
        DisplayStatText();

        End();
    }
    void End() //Performs ending actions for both complete and cancel
    {
        lapTimer.Reset();
        DelayAction(() => { mainTimeText.gameObject.SetActive(false); }, 3);
        catsCollected = 0;
    }

    public void Cancel() //Cancels obstacle course
    {
        checkpoints[targetIndex].queue = -1;
        checkpoints[targetIndex+1].queue = -1;
        checkpoints[targetIndex+2].queue = -1;

        End();
    }

    public void DisplayStatText()
    {
        string t = float.IsNaN(finalTime) ? "..." : TimeToText(finalTime);
        string bt = float.IsNaN(bestTime) ? "..." : TimeToText(bestTime);
        statsTxt.text = "Time: "+t+"\nCats Collected: "+catsCollected+"\nBest Time: "+bt+"\nTotal Cats Collected: "+totalCatsCollected;
    }

    public void CollectCat(GameObject cat)
    {
        catsCollected++;
        totalCatsCollected++;

        for (int i = 0; i < cats.Length; i++)
        {
            if (cats[i] == cat)
            {
                catIndex = i;
                break;
            }
        }

        //Get the time
        float time = (float)timer.Elapsed.TotalSeconds;

        txtOverride = true;
        mainTimeText.text = "Cat " + (catIndex + 1) + " collected at " + TimeToText(time);
        DelayAction(() => { txtOverride = false; }, 1.5f);
    }

    string TimeToText(float seconds)
    {
        string txt = seconds.ToString();
        if (!txt.Contains('.')) return txt + ".000";
        else
        {
            int beforeLen = txt.IndexOf('.');
            string afterTxt = txt[(beforeLen + 1)..];
            return (afterTxt.Length) switch
            {
                1 => txt + "00",
                2 => txt + "0",
                3 => txt,
                _ => txt[..(beforeLen + 4)]
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.IsRunning && !txtOverride) 
        {
            mainTimeText.text = TimeToText((float)timer.Elapsed.TotalSeconds);
        }
    }

    private void DelayAction(Action action, float delay){StartCoroutine(delayAction(action, delay));}
    IEnumerator delayAction(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}
