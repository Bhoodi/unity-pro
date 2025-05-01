using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [Header("Prefab & Count")]
    public GameObject dronePrefab;
    public int numberToSpawn = 5;

    [Header("Horizontal Distance (fra spawner)")]
    public float minSpawnDistance = 20f;
    public float maxSpawnDistance = 100f;

    [Header("Vertical Height Range")]
    public float minSpawnHeight = 1f;
    public float maxSpawnHeight = 5f;

    void Start()
    {
        if (dronePrefab == null) return;

        for (int i = 0; i < numberToSpawn; i++)
        {
            // Vælg en tilfældig retning (venstre eller højre)
            float sign = Random.value < 0.5f ? -1f : 1f;
            // Og en tilfældig vandret afstand
            float dist = Random.Range(minSpawnDistance, maxSpawnDistance);
            float x = transform.position.x + sign * dist;

            // Tilfældig højde inden for range
            float y = Random.Range(minSpawnHeight, maxSpawnHeight);

            Vector2 spawnPos = new Vector2(x, y);
            Instantiate(dronePrefab, spawnPos, Quaternion.identity);
        }
    }
}
