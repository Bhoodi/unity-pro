using UnityEngine;

public class PlaneCollisionHandler : MonoBehaviour
{
    [Header("Game Over UI")]
    [Tooltip("Træk dit Game Over-Canvas (root GameObject) ind her – det skal hedde 'Game Over' og være deaktiveret i Editor")]
    public Canvas gameOverCanvas;

    private bool isGameOver = false;
    private PlaneController planeController;

    void Start()
    {
        // Cache reference til controller, så vi kan deaktivere den senere
        planeController = GetComponent<PlaneController>();

        // Sørg for at hele Canvas'et er slået fra ved spilstart
        if (gameOverCanvas != null)
            gameOverCanvas.gameObject.SetActive(false);
        else
            Debug.LogError("GameOverCanvas er ikke sat i Inspector!");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGameOver) return;

        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("Collision with Water → GameOver()");
            GameOver();
        }
        else if (collision.gameObject.CompareTag("Bird"))
        {
            Debug.Log("Collision with Bird → slow down");
            if (planeController != null)
                planeController.AddSpeed(-2f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isGameOver) return;

        if (other.gameObject.CompareTag("Water"))
        {
            Debug.Log("Trigger collision with Water → GameOver()");
            GameOver();
        }
        else if (other.gameObject.CompareTag("Bird"))
        {
            Debug.Log("Trigger collision with Bird → slow down");
            if (planeController != null)
                planeController.AddSpeed(-2f);
        }
    }

    private void GameOver()
    {
        isGameOver = true;

        // 1) Pause al fysik og opdateringer
        Time.timeScale = 0f;

        // 2) Deaktiver flyets egen controller, så det ikke kan flytte sig
        if (planeController != null)
            planeController.enabled = false;

        // 3) Aktivér hele Canvas'et
        gameOverCanvas.gameObject.SetActive(true);

        // 4) Sikr Canvas' sorting order er høj nok
        gameOverCanvas.sortingOrder = 100;

        // 5) Slet flyet efter et øjeblik
        Destroy(gameObject, 0.02f);
    }
}
