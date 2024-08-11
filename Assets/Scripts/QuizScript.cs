using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR;

public class QuizScript : MonoBehaviour
{
    public bool startHidden;

    [Header("-Question & Answers-")]
    public string question;
    public string[] answers;
    [Tooltip("Index of correct answer in Answers array, starting at 0")]
    public int correctAnswerIndex=0;

    [Header("-Answer Event/Response-")]
    public string correctText;
    public Button.ButtonClickedEvent correctEvent;
    public string incorrectText;
    public Button.ButtonClickedEvent incorrectEvent;

    [Header("-Prefabs-")]
    public GameObject canvasPrefab;
    public GameObject buttonPrefab;

    //Private properties
    private GameObject canvasObj;
    private Canvas canvas;
    private TMP_Text canvasText;
    private Button[] answerButtons;
    private Transform bgTransform;
    private Transform layoutGroup;

    // Start is called before the first frame update
    void Start()
    {
        
        canvasObj = Instantiate(canvasPrefab,transform);
        canvas = canvasObj.GetComponent<Canvas>();

        bgTransform = canvasObj.transform.GetChild(0);
        canvasText = bgTransform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        layoutGroup = bgTransform.GetChild(1);

        CreateButtons();
        SetText(question);
        if(startHidden) gameObject.SetActive(false);
    }
    void CreateButtons()
    {
        answerButtons = new Button[answers.Length];
        for(int i=0; i<answers.Length; i++)
        {
            GameObject btn = Instantiate(buttonPrefab, layoutGroup);
            answerButtons[i] = btn.GetComponent<Button>();
            btn.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = answers[i];

            if (i == correctAnswerIndex)
            {
                answerButtons[i].onClick = correctEvent;
                answerButtons[i].onClick.AddListener(CorrectPressed);
            }
            else
            {
                answerButtons[i].onClick = incorrectEvent;
                answerButtons[i].onClick.AddListener(IncorrectPressed);
            }
        }
    }
    void SetText(string txt){canvasText.text = txt;}
    void ClearButtons()
    {
        int c = layoutGroup.childCount;
        for (int i = 0; i < c; i++)
            Destroy(layoutGroup.GetChild(i).gameObject);
    }
    void CorrectPressed()
    {
        SetText(correctText);
        ClearButtons();
    }
    void IncorrectPressed()
    {
        SetText(incorrectText);
        ClearButtons();
    }
}
