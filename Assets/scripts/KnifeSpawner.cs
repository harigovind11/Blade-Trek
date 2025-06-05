using System;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    public static KnifeSpawner instance;

    [SerializeField] Vector2 spawnKnife;
    [SerializeField] GameObject knife;

    private bool canSpawn = true;  // <-- control variable

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        SpawnKnife();
    }

    public void SpawnKnife()
    {
        if (!canSpawn) return;  // <- prevent spawning

        Instantiate(this.knife, spawnKnife, Quaternion.identity);
    }

    // Call this when hit point is reached
    public void StopSpawning()
    {
        canSpawn = false;
    }

    // (Optional) You can reset spawning
    public void ResumeSpawning()
    {
        canSpawn = true;
    }
}