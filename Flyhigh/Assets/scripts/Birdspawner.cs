using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [Header("Prefab & Count")]
    [Tooltip("Dronen eller fuglen, der skal spawnes")]
    public GameObject dronePrefab;
    [Tooltip("Hvor mange droner/fugle der skal spawnes ved start")]
    public int numberToSpawn = 5;

    [Header("Horizontal Distance (fra spawner)")]
    [Tooltip("Min afstand (vandret) fra spawner")]
    public float minSpawnDistance = 20f;
    [Tooltip("Max afstand (vandret) fra spawner")]
    public float maxSpawnDistance = 100f;

    [Header("Vertical Height Range")]
    [Tooltip("Laveste højde (Y) dronen kan spawne på")]
    public float minSpawnHeight = 1f;
    [Tooltip("Højeste højde (Y) dronen kan spawne på")]
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
