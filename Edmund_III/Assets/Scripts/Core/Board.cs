using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
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

    bool IsWithinBoard(int x, int y)
    {
        //ASSUMES THE SHAPE DOES NOT MOVE VERTICALLY INDEFINETLY (CAN BE IMPROVED BUT WORKS FOR THIS)
        return (x >= 0 && x < m_gridWith && y >= 0);
    }

    bool IsOccupied(int x, int y, Shape shape)
    {
        // TRUE: IF GRID SPACE CONTAINS SOMETHING THAT ISNT NOTHING AND HAS A PARENT THAT'S FROM A DIFF SHAPE OBJECT
        // FALSE: IF GRID SPACE IS EMPTY OR BELONGS TO EXISTING SHAPE THAT IS GETTING TESTED
        return (m_grid[x, y] != null && m_grid[x, y].parent != shape.transform);
    }

    public bool IsValidPosition(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = Vectorf.Round(child.position);

            if (!IsWithinBoard((int) pos.x, (int) pos.y))
            {
                return false;
            }

            if (IsOccupied((int) pos.x, (int) pos.y, shape))
            {
                return false;
            }
        }

        return true;
    }


//DRAW EMPTY GRID WITH EMPTY SPRIT OBJ
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


    public void StoreShapeInGrid(Shape shape)
    {
        if (shape == null)
        {
            return;
        }

        foreach (Transform child in shape.transform)
        {
            Vector2 pos = Vectorf.Round(child.position);
            m_grid[(int) pos.x, (int) pos.y] = child;
        }
    }

    bool IsComplete(int y)
    {
        for (int x = 0; x < m_gridWith; x++)
        {
            if (m_grid[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    void ClearRow(int y)
    {
        for (int x = 0; x < m_gridWith; x++)
        {
            if (m_grid[x, y] != null)
            {
                Destroy(m_grid[x, y].gameObject);
            }

            m_grid[x, y] = null;
        }
    }

    void ShiftRow(int y)
    {
        for (int x = 0; x < m_gridWith; x++)
        {
            if (m_grid[x, y] != null)
            {
                m_grid[x, y - 1] = m_grid[x, y];
                m_grid[x, y] = null;
                m_grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    void ShiftRows(int startY)
    {
        for (int i = startY; i < m_gridHieght; ++i)
        {
            ShiftRow(i);
        }
    }

    public void ClearAllRows()
    {
        for (int y = 0; y < m_gridHieght; ++y)
        {
            if (IsComplete(y))
            {
                ClearRow(y);

                ShiftRows(y + 1);

                y--;
            }
        }
    }
    
    public bool IsOverLimit(Shape shape)
    {
        foreach (Transform child in shape.transform) 
        {
            if (child.transform.position.y >= m_gridHieght - m_header) //-1
            {
                return true;
            }
        }
        return false;
    }
}