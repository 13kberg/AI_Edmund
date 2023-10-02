using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //HUMAN PLAYER
    playerBoard m_gameBoardPlayer;
    Spawner m_spawnerPlayer;
    Shape m_activeShapePlayer;
    
    //AI
    playerBoard m_gameBoardAi;
    Spawner m_spawnerAi;
    Shape m_activeShapeAi;

    // Start is called before the first frame update
    void Start()
    {
        //PLAYER
        m_gameBoardPlayer = GameObject.FindWithTag("BoardPlayer").GetComponent<playerBoard>();
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

        if (m_activeShapePlayer)
        {
            m_activeShapePlayer.moveDown();
        }
        
    }
}

/*TODO:
 - REMAKE METHODS FOR AI BOARD/SPAWNER*/ 