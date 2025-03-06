using UnityEngine;

public class WindBoostSpawn : MonoBehaviour
{
    // Ændret type til WindBooster, så den passer til prefab'ens script
    public WindBooster windBoost;
    public float minX = 10f;
    public float maxX = 50f;
    public float minY = -2f;
    public float maxY = 3f;

    void Start()
    {
        SpawnWindBoost();
    }

    void SpawnWindBoost()
    {
        // Spawn to separate WindBooster objekter ved hver deres tilfældige position
        float randomX1 = Random.Range(minX, maxX);
        float randomY1 = Random.Range(minY, maxY);
        Instantiate(windBoost, new Vector2(randomX1, randomY1), Quaternion.identity);

        float randomX2 = Random.Range(minX, maxX);
        float randomY2 = Random.Range(minY, maxY);
        Instantiate(windBoost, new Vector2(randomX2, randomY2), Quaternion.identity);
    }
}
