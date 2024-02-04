using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogHealth : MonoBehaviour
{
    public static LogHealth instance;
    
    [SerializeField]  int hitpoint = 8;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

      
    }

    public void updateHealth()
    {  if (hitpoint <= 0)
             {
              //TODO:Next Level
                 
             }
        hitpoint--;
    }
}