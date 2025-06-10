using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    public static KnifeSpawner instance;

    [SerializeField] Vector2 spawnKnife;
    [SerializeField] GameObject knife;

    private bool canSpawn = true;

    public KnifeThrow currentKnife; 
   
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        SpawnKnife();
    }



    public void SpawnKnife()
    {
        if (!canSpawn) return;

        GameObject knifeObj = Instantiate(knife, spawnKnife, Quaternion.identity);
        currentKnife = knifeObj.GetComponent<KnifeThrow>();

     
    }

    public void StopSpawning() => canSpawn = false;
    public void ResumeSpawning() => canSpawn = true;
}