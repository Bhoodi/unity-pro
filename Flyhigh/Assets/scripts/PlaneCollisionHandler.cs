using UnityEngine;

public class PlaneCollisionHandler : MonoBehaviour
{
    // Reference til Game Over-panelet, som skal ligge i et separat Canvas
    public GameObject gameOverPanel;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Hvis flyet rammer vandet, skal det ødelægges
        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("Collision with Water");
            GameOver();
        }
        // Hvis flyet rammer en fugl, mister flyet hastighed
        else if (collision.gameObject.CompareTag("Bird"))
        {
            Debug.Log("Collision with Bird");
            PlaneController pc = GetComponent<PlaneController>();
            if (pc != null)
            {
                // Reducér flyets vandrette hastighed med 2 enheder
                pc.AddSpeed(-2f);
            }
        }
    }

    // Hvis du bruger triggere, kan du med fordel have samme logik
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            Debug.Log("Trigger collision with Water");
            GameOver();
        }
        else if (other.gameObject.CompareTag("Bird"))
        {
            Debug.Log("Trigger collision with Bird");
            PlaneController pc = GetComponent<PlaneController>();
            if (pc != null)
            {
                pc.AddSpeed(-2f);
            }
        }
    }

    void GameOver()
    {
        // Aktiver Game Over-panelet, som ligger i et separat Canvas
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            gameOverPanel.transform.SetAsLastSibling(); // Sikrer, at panelet vises øverst
            Debug.Log("Game Over-panel activated.");
        }
        
        // Forsink destruktionen af flyet, så UI'et får tid til at vise sig
        Invoke("DestroyPlane", 0.001f);
    }

    void DestroyPlane()
    {
        Destroy(gameObject);
    }
}
