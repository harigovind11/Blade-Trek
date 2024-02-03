using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    public static KnifeSpawner instance;
    
    [SerializeField]  Vector2 spawnKnife;
    [SerializeField]  GameObject knife;

    private void Awake()
    {
          if (instance == null)
                {
                    instance = this;
                }
    }

    void OnEnable()
    {
        SpawnKnife();

    }

 public   void SpawnKnife()
    {
        
        GameObject knife = Instantiate(this.knife,spawnKnife,Quaternion.identity);
    }
}
