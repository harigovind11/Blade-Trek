using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    
    [SerializeField]  GameObject gameOverCanvas;
    [SerializeField]  GameObject buttonsCanvas;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

  public  void GameOver()
    {
        buttonsCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
        
    }

   public void ReloadGame()
   {
       Time.timeScale = 1f;
       int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

   public void LoadNextLevel()
   {

       int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       int nextSceneIndex = currentSceneIndex + 1;
       if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
       {
           nextSceneIndex = 0;
       }
       SceneManager.LoadScene(nextSceneIndex);
   }

 
   public void QuitGame()
   {
       Application.Quit();
   }
}
