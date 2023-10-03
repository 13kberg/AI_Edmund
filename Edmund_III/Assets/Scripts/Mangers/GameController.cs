using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //HUMAN PLAYER
    Board m_gameBoardPlayer;
    Spawner m_spawnerPlayer;
    Shape m_activeShapePlayer;
    float m_dropIntervalPlayer = 0.25f;
    float m_timeToDropPlayer;

    //AI
    // playerBoard m_gameBoardAi;
    // Spawner m_spawnerAi;
    // Shape m_activeShapeAi;
    // float m_dropIntervalAi = 0.25f;
    // float m_timeToDropAI;

    // Start is called before the first frame update
    void Start()
    {
        //PLAYER
        m_gameBoardPlayer = GameObject.FindWithTag("BoardPlayer").GetComponent<Board>();
        m_spawnerPlayer = GameObject.FindWithTag("SpawnerPlayer").GetComponent<Spawner>();


        if (m_spawnerPlayer)
        {
            if (m_activeShapePlayer == null)
            {
                m_activeShapePlayer = m_spawnerPlayer.spawnShape();
            }

            m_spawnerPlayer.transform.position = Vectorf.Round(m_spawnerPlayer.transform.position);
        }

        //SAFETY CHECKS 
        if (!m_gameBoardPlayer)
        {
            Debug.Log("WARNING: NO PLAYER GAME BOARD IS DEFINED");

            if (!m_spawnerPlayer)
            {
                Debug.Log("WARNING: NO PLAYER SPAWNER IS DEFINED");
            }
        }

        //AI
    }

    // Update is called once per frame
    void Update()
    {
        //IF NO GAMEBOARD OR SPAWNER (EITHER AI OR USER) DONT PLAY GAME
        if (!m_gameBoardPlayer || !m_spawnerPlayer) //|| !m_spawnerAi || ! m_gameBoardAi)
        {
            return;
        }

        if (Time.time > m_timeToDropPlayer)
        {
            m_timeToDropPlayer = Time.time + m_dropIntervalPlayer;
            if (m_activeShapePlayer)
            {
                m_activeShapePlayer.moveDown();
                if (!m_gameBoardPlayer.IsValidPosition(m_activeShapePlayer))
                {
                    //SHAPE LANDING
                    m_activeShapePlayer.moveUp();
                    m_gameBoardPlayer.StoreShapeInGrid(m_activeShapePlayer);

                    if (m_spawnerPlayer)
                    {
                        m_activeShapePlayer = m_spawnerPlayer.spawnShape();
                    }
                    
                }
            }
        }
    }
}

/*TODO:
 - REMAKE METHODS FOR AI BOARD/SPAWNER*/