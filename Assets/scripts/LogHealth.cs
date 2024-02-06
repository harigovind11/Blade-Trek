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
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        updateDisplay();
    }

    public void updateHealth()
    {  if (hitpoint <= 1)
             {
             GameManager.instance.LoadNextLevel();
                 
             }
        hitpoint--;
        updateDisplay();

    }

    void updateDisplay()
    {
        count.text = hitpoint.ToString();
    }
}