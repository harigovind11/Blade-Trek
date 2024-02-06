using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogHealth : MonoBehaviour
{
    public static LogHealth instance;
    
    [SerializeField] public int hitpoint = 8;
    [SerializeField] TextMeshProUGUI count;
    
    [SerializeField]  AudioClip woodChop;
    [SerializeField]  AudioClip death;
    [SerializeField]  AudioClip levelUp;
    
    private AudioSource _audioSource;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _audioSource = GetComponent<AudioSource>();
        UpdateDisplay();
    }
    

    public void UpdateHealth()
    {  if (hitpoint == 1)
        {
      
            StartCoroutine(LoadNextLevel());
        }
        // _audioSource.Stop();
        _audioSource.PlayOneShot(woodChop);
        hitpoint--;
        UpdateDisplay();

    }

    public void GameOverAudio()
    {
       _audioSource.Stop(); 
       _audioSource.PlayOneShot(death);
    }
    
     IEnumerator LoadNextLevel()
    {
  
    
        
        _audioSource.Stop();
        _audioSource.PlayOneShot(levelUp);

    
        yield return new WaitForSeconds(1f);


    
       
        GameManager.instance.LoadNextLevel();
    }

   

    void UpdateDisplay()
    {
        count.text = hitpoint.ToString();
    }
}