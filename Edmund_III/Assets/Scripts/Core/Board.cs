using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform m_gridEmptySquare;
    public int m_gridHieght = 30;
    public int m_gridWith = 10;

    //Private by defualt
    // Start is called before the first frame update
    void Start()
    {
        DrawEmptyCell();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void DrawEmptyCell()
    {
        for (int y = 0; y < m_gridHieght; y++)
        {
            for (int x = 0; x < m_gridWith; x++)
            {
                Transform clone;
                clone = Instantiate(m_gridEmptySquare,new Vector3(x, y, 0), Quaternion.identity) as Transform;
                clone.name = "Board space (x = " + x.ToString() + "y = " + y.ToString() + " )";
                clone.transform.parent.transform;
            }
        }
    }
}