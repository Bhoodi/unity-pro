using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Platform spawning settings
    public GameObject platformPrefab;
    public float minSpawnDistance = 20f;
    public float maxSpawnDistance = 50f;
    public float platformY = -3.6f;

    // Win UI settings
    public GameObject winPanel;

    private bool gameWon = false;

    void Start()
    {
        // Sørg for spillet kører normalt ved start
        Time.timeScale = 1f;
        if (winPanel != null) winPanel.SetActive(false);
        SpawnPlatform();
    }

    void SpawnPlatform()
    {
        if (platformPrefab == null)
        {
            Debug.LogError("PlatformPrefab ikke sat i Inspector");
            return;
        }

        float randomX = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnPos = new Vector2(randomX, platformY);
        Instantiate(platformPrefab, spawnPos, Quaternion.identity);
    }

    public void WinGame()
    {
        if (gameWon) return;
        gameWon = true;

        // Pause al fysik & opdateringer
        Time.timeScale = 0f;

        // Vis dit “You Win”‑panel
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            winPanel.transform.SetAsLastSibling();
        }
    }
}
