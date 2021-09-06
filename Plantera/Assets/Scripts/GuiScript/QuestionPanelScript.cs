using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanelScript : MonoBehaviour
{
    public Text questionText;
    public Text[] option;
    public int answerButton;
    public GameObject correctPanel;
    public GameObject wrongPanel;
    public GameObject endPanel;
    public Text scoreText;

    public bool isQuestioning;
    public int points;
    public Question[] questions;
    public int currentQuestion;

    public AudioClip questionBGM;
    public AudioClip endBGM;
    [SerializeField] AudioClip correct;
    [SerializeField] AudioClip wrong;

    private void Start()
    {
        SoundManager.instance.PlayMusic(questionBGM);
        StartQuestion();
    }
    public void InitializeQuestion(Question question)
    {
        questionText.text = question.questionText;
        bool[] ans = new bool[4];
        for (int i = 0; i < option.Length; i++)
        {
            int ansNo;
            do
            {
                ansNo = Random.Range(0, 4);
                if (!ans[ansNo])
                {
                    ans[ansNo] = true;
                    break;
                }
            } while (true);

            option[i].text = question.answer[ansNo];
            if (ansNo == 0)
            {
                answerButton = i;
            }
        }
       
    }
    public void ButtonAnswerPressed(int x)
    {
        if (isQuestioning)
        {
            isQuestioning = false;
            if (x == answerButton)
            {
                Debug.Log("Correct");
                SoundManager.instance.PlaySfx(correct);
                correctPanel.SetActive(true);
                points += 100;
            }
            else {
                Debug.Log("Wrong");
                SoundManager.instance.PlaySfx(wrong);
                wrongPanel.SetActive(true);
            }
            StartCoroutine(NextQuestion());
        }
    }
    public void StartQuestion()
    {
        currentQuestion = 0;
        points = 0;
        InitializeQuestion(questions[currentQuestion]);
        isQuestioning = true;
        correctPanel.SetActive(false);
        wrongPanel.SetActive(false);
        endPanel.SetActive(false);
    }
    IEnumerator NextQuestion()
    {
        float delay = 2f;
        while (delay > 0)
        {
            delay -= Time.deltaTime;
            yield return null;
        }
        correctPanel.SetActive(false);
        wrongPanel.SetActive(false);
        currentQuestion++;
        if (currentQuestion < questions.Length)
        {
            InitializeQuestion(questions[currentQuestion]);
            isQuestioning = true;
        }
        else
        {
            endPanel.SetActive(true);
            scoreText.text = "Markah akhir anda ialah " + points;
            PlayerManager.instance.playerStat.score = points;
            SoundManager.instance.PlayMusic(endBGM);
            PlayerManager.instance.playerStat.answeredLastQuestion = true;
        }
    }
    public void EndQuestionPanel()
    {
        Destroy(gameObject);
    }
}
[System.Serializable]
public class Question
{
    [TextArea(5,8)]
    public string questionText;
    public string[] answer;
}
