using UnityEngine;

public class WindBoostSpawn : MonoBehaviour
{
    public WindBooster windBoostPrefab; // Prefab for the wind boost
    public int numberToSpawn = 2;   // Number of wind boosts skal spawne

    [Header("Spawn Range")]
    public float minX = 10f;
    public float maxX = 50f;
    public float minY = -2f;
    public float maxY = 3f;

    void Start()
    {
        SpawnWindBoosts();
    }

    void SpawnWindBoosts()
    {
        if (windBoostPrefab == null) return;

        for (int i = 0; i < numberToSpawn; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Vector2 spawnPos = new Vector2(randomX, randomY);
            Instantiate(windBoostPrefab, spawnPos, Quaternion.identity);
        }
    }
}
