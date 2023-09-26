using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBoard : MonoBehaviour
{
    //BOARD VARIABLES
    public Transform m_gridEmptySprite;
    public int m_gridHieght = 30;
    public int m_gridWith = 10;

    public int m_header = 8;

    //GAME PIECES VARIABLES 
    Transform[,] m_grid;

    private void Awake()
    {
        m_grid = new Transform[m_gridWith, m_gridHieght];
    }

    // Start is called before the first frame update
    void Start()
    {
        DrawEmptyCell();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //DEFUALT-PRIVATE 
    void DrawEmptyCell()
    {
        if (m_gridEmptySprite != null)
        {
            for (int y = 0; y < m_gridHieght - m_header; y++)
            {
                for (int x = 0; x < m_gridWith; x++)
                {
                    Transform clone;
                    clone = Instantiate(m_gridEmptySprite, new Vector3(x, y, 0), Quaternion.identity) as Transform;
                    clone.name = "Board space (x = " + x.ToString() + "y = " + y.ToString() + " )";
                    clone.transform.parent = transform;
                }
            }
        }
        else
        {
            Debug.Log("NO SPRITE OBJECT ASSIGNED");
        }
    }
}