using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public bool m_canRotate = true;

    void move(Vector3 moveDirection)
    {
        transform.position += moveDirection;
    }

    public void moveLeft()
    {
        move(new Vector3(-1, 0, 0));
    }

    public void moveRight()
    {
        move(new Vector3(1, 0, 0));
    }

    public void moveDown()
    {
        move(new Vector3(0, -1, 0));
    }

    public void moveUp()
    {
        move(new Vector3(0, 1, 0));
    }

    public void rotateRight()
    {
        if (m_canRotate)
        {
            transform.Rotate(0, 0, -90);
        }
    }

    public void rotateLeft()
    {
        if (m_canRotate)
        {
            transform.Rotate(0, 0, 90);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //TESTING METHODS
        // InvokeRepeating("moveDown",0,0.5f);
        // InvokeRepeating("rotateRight",0,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}