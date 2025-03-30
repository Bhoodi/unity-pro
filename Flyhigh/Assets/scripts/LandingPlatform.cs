using UnityEngine;

public class LandingPlatform : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Hvis objektet har tag "Plane" eller "Helicopter"
        if (collision.gameObject.CompareTag("Plane") || collision.gameObject.CompareTag("Helicopter")|| collision.gameObject.CompareTag("Jet"))
        {
            Debug.Log(collision.gameObject.tag + " has landed on the platform - You win!");
            // Find GameManager og kald WinGame()
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.WinGame();
            }
        }
    }
}
