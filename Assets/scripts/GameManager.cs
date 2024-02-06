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
   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

  public  void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
        
    }

   public void ReloadGame()
   {
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
