using UnityEngine;
using TMPro;
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
    // Referer til din TextMeshPro-komponent, som skal vise "YOU WIN"
    public TMP_Text winText;

    void Start()
    {
        SpawnPlatform();
        
        // Sørg for, at winText er deaktiveret ved spilstart
        if (winText != null)
        {
            winText.gameObject.SetActive(false);
        }
    }

    void SpawnPlatform()
    {
        float randomX = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 spawnPos = new Vector2(randomX, platformY);
        currentPlatform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);
    }

    // Denne metode kaldes, når flyet lander korrekt og vinder spillet
    public void WinGame()
    {
        Debug.Log("Game Won!");
        if (winText != null)
        {
            winText.gameObject.SetActive(true);
            winText.text = "YOU WIN";
            // Sørg for, at winText vises øverst i Canvas-hierarkiet
            winText.transform.SetAsLastSibling();
        }
    }
}
