using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    public static KnifeSpawner instance;

    [SerializeField] Vector2 spawnKnife;
    [SerializeField] GameObject knife;

    private bool canSpawn = true;

    public KnifeThrow currentKnife;  // <-- track current knife
    public AimLineController aimLineController; 
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

        // Set aim line to follow the new knife
        if (aimLineController != null)
            aimLineController.knifeTransform = currentKnife.transform;
    }

    public void StopSpawning() => canSpawn = false;
    public void ResumeSpawning() => canSpawn = true;
}