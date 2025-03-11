using UnityEngine;

public class LandingPlatform : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Antag, at dit fly har tagget "Plane" eller indeholder et PlaneController-script
        if (collision.gameObject.CompareTag("Plane"))
        {
            Debug.Log("Plane has landed on the platform - You win!");
            // Find GameManager og kald WinGame()
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.WinGame();
            }
        }
    }
}
