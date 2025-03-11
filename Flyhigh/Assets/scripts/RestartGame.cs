using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // Denne metode kaldes, når restart-knappen trykkes
    public void Restart()
    {
        // Genindlæs den aktive scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
