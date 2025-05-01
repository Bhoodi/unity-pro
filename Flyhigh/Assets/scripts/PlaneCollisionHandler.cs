using UnityEngine;

public class PlaneCollisionHandler : MonoBehaviour
{
    public Canvas gameOverCanvas;
    public GameObject explosionPrefab;

    private bool isGameOver = false;

    void Start()
    {
        if (gameOverCanvas != null)
            gameOverCanvas.gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGameOver) return;

        if (collision.gameObject.CompareTag("Water"))
        {
            TriggerGameOver();
        }
        else if (collision.gameObject.CompareTag("Bird"))
        {
            var pc = GetComponent<PlaneController>();
            if (pc != null)
                pc.AddSpeed(-2f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isGameOver) return;

        if (other.gameObject.CompareTag("Water"))
        {
            TriggerGameOver();
        }
        else if (other.gameObject.CompareTag("Bird"))
        {
            var pc = GetComponent<PlaneController>();
            if (pc != null)
                pc.AddSpeed(-2f);
        }
    }

    private void TriggerGameOver()
    {
        isGameOver = true;

        // Aktiver Game Over UI
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(true);
            gameOverCanvas.sortingOrder = 100;
        }

        // Spawn explosion i flyets position
        if (explosionPrefab != null)
        {
            GameObject expl = Instantiate(
                explosionPrefab, 
                transform.position, 
                Quaternion.identity
            );
            
        
        }

        // Ødelæg flyet
        Destroy(gameObject);
    }
}
