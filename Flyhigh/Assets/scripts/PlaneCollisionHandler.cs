using UnityEngine;

public class PlaneCollisionHandler : MonoBehaviour
{
    [Header("Game Over UI")]
    [Tooltip("Træk dit Game Over-Canvas (root GameObject) ind her – det skal hedde 'Game Over' og være deaktiveret i Editor")]
    public Canvas gameOverCanvas;

    [Header("Explosion Effect")]
    [Tooltip("Træk dit Explosion-prefab (Particle System eller Animated Sprite) ind her")]
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

        // 1) Aktiver Game Over UI
        if (gameOverCanvas != null)
        {
            gameOverCanvas.gameObject.SetActive(true);
            gameOverCanvas.sortingOrder = 100;
        }

        // 2) Spawn explosion i flyets position
        if (explosionPrefab != null)
        {
            GameObject expl = Instantiate(
                explosionPrefab, 
                transform.position, 
                Quaternion.identity
            );
            
        
        }

        // 3) Ødelæg flyet
        Destroy(gameObject);
    }
}
