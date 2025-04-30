using UnityEngine;

public class WindBoostSpawn : MonoBehaviour
{
    [Header("Prefab & Count")]
    [Tooltip("Prefab med dit WindBooster-script")]
    public WindBooster windBoostPrefab;
    [Tooltip("Hvor mange wind-boostere der skal spawne ved start")]
    public int numberToSpawn = 2;

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
