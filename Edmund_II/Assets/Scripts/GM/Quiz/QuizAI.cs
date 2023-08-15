using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizAI : MonoBehaviour
{
    // AI is called E.D.M.U.N.D

    public int[] answers;
    public Text Textbox;
    public Button[] buttons;
    public Button conButton;


    void Start()
    {
        answers = new int[20];
    }

    public void SplitResultIntoSets()
    {
        List<List<int>> resultSets = new List<List<int>>();

        for (int i = 0; i < 4; i++)
        {
            List<int> resultSet = new List<int>();
            for (int j = i * 5; j < (i + 1) * 5; j++)
            {
                resultSet.Add(answers[j]);
            }

            resultSets.Add(resultSet);
        }


        // PRINTING RESULTS SET
        // for (int i = 0; i < resultSets.Count; i++)
        // {
        //     Debug.Log("Set " + (i + 1) + ": " + string.Join(",", resultSets[i]));
        // }

        // FIND SET W/ HIGHEST VALUE AND RETURN INDEX OF resultSets
        int highestSum = int.MinValue;
        int highestSumSetIndex = -1;

        for (int i = 0; i < resultSets.Count; i++)
        {
            int sum = 0;
            foreach (int number in resultSets[i])
            {
                sum += SumDigits(number);
            }

            if (sum > highestSum)
            {
                highestSum = sum;
                highestSumSetIndex = i;
            }
        }

        //DETERMINNE THE ATTACHMENT TYPE \\ BUTTON[4] IS CONTINUE BUTTON. 
        
        if (highestSumSetIndex == 0) //Secure-The Protector
        {
            // Debug.Log("Secure-The Protector");
            Textbox.text = "Secure-The Protector";
            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(false);
            }

            conButton.gameObject.SetActive(true);
        }
        else if (highestSumSetIndex == 1) //Anxious-The Worrier
        {
            // Debug.Log("Anxious-The Worrier");
            Textbox.text = "Anxious-The Worrier";
            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(false);
            }

            conButton.gameObject.SetActive(true);
        }
        else if (highestSumSetIndex == 2) //Disorganized-The Hesitant
        {
            // Debug.Log("Disorganized-The Hesitant");
            Textbox.text = "Disorganized-The Hesitant";
            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(false);
            }

            conButton.gameObject.SetActive(true);
        }
        else if (highestSumSetIndex == 3) //Avoidant-The Dissmisser
        {
            // Debug.Log("Avoidant-The Dissmisser");
            Textbox.text = "Avoidant-The Dissmisser";
            
            foreach (Button button in buttons)
            {
                button.gameObject.SetActive(false);
            }

            conButton.gameObject.SetActive(true); 
        }
    }


    private int SumDigits(int number)
    {
        int sum = 0;
        while (number != 0)
        {
            sum += number % 10;
            number /= 10;
        }

        return sum;
    }
}