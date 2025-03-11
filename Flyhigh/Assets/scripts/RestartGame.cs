using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Reference til Game Over-panelet (placér panelet i et separat Canvas)
    public GameObject gameOverPanel;
    
    // Kaldes, når spillet skal afsluttes (f.eks. ved kollision)
    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }
    
    // Denne metode kaldes, når restart-knappen trykkes
    public void Restart()
    {
        // Genindlæs den aktive scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
