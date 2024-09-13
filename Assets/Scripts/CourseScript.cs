using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class CourseScript : MonoBehaviour
{
    //---Public serialized fields---
    public Checkpoint[] checkpoints;
    public GameObject[] cats;

    public TMP_Text mainTimeText;
    //---

    //---Private fields---

    //Timer stuff
    private Stopwatch timer = new();
    private Stopwatch lapTimer = new();
    float[] laps; //in seconds
    float finalTime;

    //Checkpoint stuff
    Checkpoint target;
    int targetIndex = 0;

    //---

    // Start is called before the first frame update
    void Start()
    {
        foreach (Checkpoint cp in checkpoints) cp.enterAction += NextCheckpoint;
        laps = new float[checkpoints.Length];
    }

    public void Begin()
    {
        targetIndex = 0;
        target = checkpoints[0];
        timer = Stopwatch.StartNew();
        lapTimer = Stopwatch.StartNew();
        //Set checkpoint queues (so checkpoints know when they are next)
        for (int i = targetIndex, cnt = 0; i < checkpoints.Length; i++, cnt++) checkpoints[i].queue = cnt;
    }

    void NextCheckpoint() //Called by checkpoint when entered
    {
        //Get the lap's time
        laps[targetIndex] = (float)lapTimer.Elapsed.TotalSeconds;
        lapTimer.Restart();

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
        checkpoints[^1].queue = -1;

        End();
    }
    void End() //Performs ending actions for both complete and cancel
    {
        lapTimer.Reset();
    }

    public void Cancel() //Cancels obstacle course
    {
        checkpoints[targetIndex].queue = -1;
        checkpoints[targetIndex+1].queue = -1;
        checkpoints[targetIndex+2].queue = -1;

        End();
    }

    string TimeToText(float seconds)
    {
        string txt = seconds.ToString();
        if (!txt.Contains('.')) return txt + ".000";
        else
        {
            int beforeLen = txt.IndexOf('.');
            string afterTxt = txt.Substring(beforeLen + 1);
            switch (afterTxt.Length)
            {
                case 1: return txt+"00";
                case 2: return txt+"0";
                case 3: return txt;
                default: return txt.Substring(0, beforeLen + 4);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.IsRunning) {
            mainTimeText.text = TimeToText((float)timer.Elapsed.TotalSeconds);
        }
    }
}
