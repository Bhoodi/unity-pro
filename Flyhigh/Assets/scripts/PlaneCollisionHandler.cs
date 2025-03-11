using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneCollisionHandler : MonoBehaviour
{
    // Referencer til game over UI-panel
    public GameObject gameOverPanel;

    // Når flyet rammer noget (brug OnCollisionEnter2D hvis dine objekter ikke er triggere)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Tjek om den kolliderede objekt har tag "Bird" eller "Water"
        if(collision.gameObject.CompareTag("Bird") || collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("Collision with " + collision.gameObject.tag);
            GameOver();
        }
    }
    
    // Alternativt, hvis dine kolliderende objekter er triggere:
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bird") || other.gameObject.CompareTag("Water"))
        {
            Debug.Log("Trigger collision with " + other.gameObject.tag);
            GameOver();
        }
    }
    
    void GameOver()
    {
        // Deaktiver eventuelt flyets kontrol (fx PlaneController)
        PlaneController pc = GetComponent<PlaneController>();
        if(pc != null)
        {
            pc.enabled = false;
        }
        
        // Vis game over-panelet med restart-knappen
        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }
}
