using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //HUMAN PLAYER
    Board m_gameBoard;
    Spawner m_spawner;
    Shape m_activeShape;

    float m_dropInterval = 0.9f;
    float m_timeToDrop;
    private float m_timeToNextKeyLeftRight;
    [Range(0.02f, 1f)] public float m_keyRepeatRateLeftRight = 0.15f;
    float m_timeToNextKeyDown;
    [Range(0.01f, 1f)] public float m_keyRepeatRateDown = 0.1f;
    float m_timeToNextKeyRotate;
    [Range(0.02f, 1f)] public float m_keyRepeatRateRotate = 0.25f;

    public GameObject m_gameOverPanel;
    bool m_gameOver = false;

    SoundController m_soundController;

    ScoreController m_scoreController;

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
        m_timeToNextKeyLeftRight = Time.time;
        m_timeToNextKeyDown = Time.time;
        m_timeToNextKeyRotate = Time.time;

        m_gameBoard = GameObject.FindWithTag("BoardPlayer").GetComponent<Board>();
        m_spawner = GameObject.FindWithTag("SpawnerPlayer").GetComponent<Spawner>();
        m_soundController = GameObject.FindObjectOfType<SoundController>();
        m_scoreController = GameObject.FindObjectOfType<ScoreController>();


        //SAFETY CHECKS 
        if (!m_gameBoard)
        {
            Debug.Log("WARNING: NO PLAYER GAME BOARD IS DEFINED");
        }

        if (!m_soundController)
        {
            Debug.Log("WARNING: NO SOUND CONTROLLER IS DEFINED");
        }

        if (!m_scoreController)
        {
            Debug.Log("WARNING: NO SCORE CONTOLLER IS DEFINED");
        }

        if (!m_spawner)
        {
            Debug.Log("WARNING: NO PLAYER SPAWNER IS DEFINED");
        }
        else
        {
            if (m_activeShape == null)
            {
                m_activeShape = m_spawner.spawnShape();
            }

            m_spawner.transform.position = Vectorf.Round(m_spawner.transform.position);
        }

        if (m_gameOverPanel)
        {
            m_gameOverPanel.SetActive(false);
        }

        //AI
    }

    // Update is called once per frame
    void Update()
    {
        //IF NO GAMEBOARD OR SPAWNER (EITHER AI OR USER) DONT PLAY GAME
        if (!m_gameBoard || !m_spawner || !m_activeShape || m_gameOver || !m_soundController ||
            !m_scoreController) //|| !m_spawnerAi || ! m_gameBoardAi)
        {
            return;
        }

        PlayerInput();
    }

    void PlayerInput()
    {
        //USER
        //RIGHTKEY
        if (Input.GetButton("MoveRight") && Time.time > m_timeToNextKeyLeftRight || Input.GetButtonDown("MoveRight"))
        {
            m_activeShape.moveRight();
            m_timeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;
            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.moveLeft();
                // Debug.Log("Hit Right Boundary ");
            }
            else
            {
                PlaySound(m_soundController.m_moved, 0.5f);
            }
        } //LEFT KEY
        else if (Input.GetButton("MoveLeft") && Time.time > m_timeToNextKeyLeftRight || Input.GetButtonDown("MoveLeft"))
        {
            m_activeShape.moveLeft();
            m_timeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;
            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.moveRight();
            }
            else
            {
                PlaySound(m_soundController.m_moved, 0.5f);
            }
        } //ROTATE CLOCKWISE
        else if (Input.GetButton("RotateC") && Time.time > m_timeToNextKeyRotate ||
                 Input.GetButton("RotateC1") && Time.time > m_timeToNextKeyRotate)
        {
            m_activeShape.rotateRight();
            m_timeToNextKeyRotate = Time.time + m_keyRepeatRateRotate;
            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.rotateLeft();
            }
            else
            {
                PlaySound(m_soundController.m_rotated, 0.5f);
            }
        } //ROTATE COUNTERCLOCKWISE
        else if (Input.GetButton("RotateCC") && Time.time > m_timeToNextKeyRotate ||
                 Input.GetButton("RotateCC1") && Time.time > m_timeToNextKeyRotate)
        {
            m_activeShape.rotateLeft();
            m_timeToNextKeyRotate = Time.time + m_keyRepeatRateRotate;
            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.rotateRight();
            }
            else
            {
                PlaySound(m_soundController.m_rotated, 0.5f);
            }
        }
        else if (Input.GetButton("MoveDown") && (Time.time > m_timeToNextKeyDown) || (Time.time > m_timeToDrop))
        {
            m_timeToDrop = Time.time + m_dropInterval;
            m_timeToNextKeyDown = Time.time + m_keyRepeatRateDown;

            m_activeShape.moveDown();
            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                if (m_gameBoard.IsOverLimit(m_activeShape))
                {
                    GameOver();
                }
                else
                {
                    LandShape();
                }
            }
        }
    }

    void GameOver()
    {
        m_activeShape.moveUp();

        if (m_gameOverPanel)
        {
            m_gameOverPanel.SetActive(true);
        }

        PlaySound(m_soundController.m_gameOver, 5f);
        m_gameOver = true;
    }

    public void Restart()
    {
        //Debug.Log("Restart");
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene("PlayerTetris");
    }

    void LandShape()
    {
        //ADD SCORE METHOD
        m_timeToNextKeyLeftRight = Time.time;
        m_timeToNextKeyDown = Time.time;
        m_timeToNextKeyRotate = Time.time;

        m_activeShape.moveUp();
        m_gameBoard.StoreShapeInGrid(m_activeShape);
        m_activeShape = m_spawner.spawnShape();

        m_gameBoard.ClearAllRows();

        PlaySound(m_soundController.m_landed);
        
        
        //WHY CONTINUSLY ADD THE SCORE
        if (m_gameBoard.m_completedRows > 0)
        {
            m_scoreController.ScoreLines(m_gameBoard.m_completedRows);
            PlaySound(m_soundController.m_lineCleared);
        }
    }

    void PlaySound(AudioClip clip, float volMultiplier = 1.0f)
    {
        if (m_soundController.m_fxEnabled && clip)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position,
                Mathf.Clamp(m_soundController.m_fxVolume * volMultiplier, 0.05f, 1f));
        }
    }
}

/*TODO:
 - REMAKE METHODS FOR AI BOARD/SPAWNER*/