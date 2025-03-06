using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Platform Spawning")]
    public GameObject platformPrefab;
    public float minSpawnDistance = 20f;
    public float maxSpawnDistance = 50f;
    public float platformY = -3.6f; // Den højde, hvor platformen skal ligge

    private GameObject currentPlatform;

    void Start()
    {
        SpawnPlatform();
    }

    void SpawnPlatform()
    {
        // Vælg en tilfældig x-position
        float randomX = Random.Range(minSpawnDistance, maxSpawnDistance);

        // Opret en ny position for platformen
        Vector2 spawnPos = new Vector2(randomX, platformY);

        // Instantiér platformen
        currentPlatform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);
    }
}
