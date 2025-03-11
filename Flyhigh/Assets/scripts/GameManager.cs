using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Platform Spawning")]
    public GameObject platformPrefab;
    public float minSpawnDistance = 20f;
    public float maxSpawnDistance = 50f;
    public float platformY = -3.6f; // Den højde, hvor platformen skal ligge

    private GameObject currentPlatform;

    [Header("Win UI")]
    // Dette GameObject skal være dit win-panel (UI), som du har oprettet i et separat Canvas
    public GameObject winPanel;

    void Start()
    {
        SpawnPlatform();
        // Sørg for, at winPanel er deaktiveret ved start
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
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

    // Kald denne metode, når flyet lander korrekt
    public void WinGame()
    {
        Debug.Log("Game Won!");
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            winPanel.transform.SetAsLastSibling(); // Sørger for, at winPanel vises øverst
        }
    }
}
