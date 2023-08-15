using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
   
    public void StartGameGm()
    {
       StartCoroutine(ChangeLevelDm());
    }
 
     public void StartGamePlayer()
    {
       StartCoroutine(ChangeLevelPlayer());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

   IEnumerator ChangeLevelDm()
{
    float fadeTime = GameObject.Find("GameController").GetComponent<FaderScript>().BeginFade(1);
    yield return new WaitForSeconds(fadeTime);
    SceneManager.LoadScene("GMQuestions", LoadSceneMode.Single);  
}

   IEnumerator ChangeLevelPlayer()
{
    float fadeTime = GameObject.Find("GameController").GetComponent<FaderScript>().BeginFade(1);
    yield return new WaitForSeconds(fadeTime);
    SceneManager.LoadScene("DungeonGeneratorPlayer", LoadSceneMode.Single);  
}
}
