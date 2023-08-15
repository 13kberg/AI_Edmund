using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public Text questionText;
    [HideInInspector] public Button[] answerButtons;
    

    private int currentQuestionIndex;

    private readonly string[] questions = new string[]
    {
        // 0-4 Secure-The Protector 5-9 \\ Anxious-The Worrier \\ 10-14 Disorganized-The Hesitant \\ 15-19 Avoidant-The Dissmisser

        "The time I get to spend with friends and family is important, they are the cause I am fighting for", //0 
        "Discretion is a skill I have honed to protect those closest to me", //1
        "I will always do my duty no matter what the price", //2
        "It is a priority to keep agreements with my party", //3
        "I like to believe people are fundamentally good", //4
        "My lack of confidence leads me to believe I am not worthy", //5
        "Mimicking is easy as I know I am giving the correct response that the situation requires", //6
        "I dont understand how people can put demands on me when they dont offer the same guarantees that I do", //7
        "But I can't tell the party that I'm scared to be alone, it could jeopardise the quest", //8
        "I lack the self confidence a warrior needs", //9
        "When i've reached a certain level of closses with my party I sometimes experience inexplicable fear.", //10
        "When a roadblock is in my way, I often feel stumped and feel they are unconquerable", //11
        "My party members often comments or complains that I am controlling.", //12
        "I Prepare for the worst, and I expect the worst", //13
        "Protection often feels out of reach. I struggle to feel safe with my party", //14
        "I cant let me down, I struggle to reach out for help", //15
        "I find myself minimizing the importance of close relationships in my life.", //16
        "It is easier for me to think things through than to express myself emotionally. ", //17
        "I usually prefer relationships with animals instead of people", //18
        "I sometimes feel superior in not needing others and wish others wouldn't rely on me too much. " //19
    };

    void Start()
    {
        // answers = new int[20];
        
        
            
        currentQuestionIndex = 0;
        DisplayQuestion();
    }

    public void OnAnswerButtonClick(int value)
    {
        QuizAI quizAI = FindObjectOfType<QuizAI>();
        
        quizAI.answers[currentQuestionIndex] = value;
        currentQuestionIndex++;

        if (currentQuestionIndex >= questions.Length)
        {
            // END OF QUIZ
            // Debug.Log("Quiz completed! Answers: " + string.Join(",", quizAI.answers));
            quizAI.SplitResultIntoSets();
        }
        else
        {
            DisplayQuestion();
        }
    }

    public void OnConButtonClick()
    {
        
    }

    private void DisplayQuestion()
    {
        questionText.text = questions[currentQuestionIndex];
        // SET ANSWER FOR CURRENT QUESTION
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = i.ToString();
            // answerButtons[i].GetComponentInChildren<Text>().text = "Answer " + (i + 1);
        }
    }
}