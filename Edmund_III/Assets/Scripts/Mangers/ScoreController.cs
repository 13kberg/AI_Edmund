using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    int m_score = 0;
    int m_lines;
    int m_level = 1;


    public int m_linesPerLevel = 5;

    public Text m_linesText;
    public Text m_levelText;
    public Text m_scoreText;

    private const int m_minLines = 1;

    private const int m_maxLines = 4;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ScoreLines(int n)
    {
        n = Mathf.Clamp(n, m_minLines, m_maxLines);

        switch (n)
        {
            case 1:
                m_score += 40 * m_level;
                break;
            case 2:
                m_score += 100 * m_level;
                break;
            case 3:
                m_score += 300 * m_level;
                break;
            case 4:
                m_score += 1200 * m_level;
                break;
        }

        UpdateUIText();
    }

    public void Reset()
    {
        m_level = 1;
        m_lines = m_linesPerLevel * m_level;
    }

    void UpdateUIText()
    {
        if (m_linesText)
        {
            m_linesText.text = m_lines.ToString();
        }

        if (m_levelText)
        {
            m_levelText.text = m_level.ToString();
        }

        if (m_scoreText)
        {
            m_scoreText.text = addZero(m_score, 5);
        }
    }


    string addZero(int n, int addDigits)
    {
        string nStr = n.ToString();

        while (nStr.Length < addDigits)
        {
            nStr = "0" + nStr;
        }

        return nStr;
    }
}

/*
 * SCORING RULES
 *
 * CLEAR 1 ROW = 40 X LEVEL
 * CLEAR 2 ROWS = 100 X LEVEL
 * CLEAR 3 ROWS = 300 X LEVEL
 * CLEAR 4 ROWS = 1200 X LEVEL
*/