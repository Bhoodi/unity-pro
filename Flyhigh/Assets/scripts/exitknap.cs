using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Denne metode kaldes, når du klikker på knappen
    public void ExitGame()
    {
        // I editoren gør dette ingenting, men i en build afslutter det applikationen
        Application.Quit();
        
        // Hvis du vil se, at det virker i editoren, kan du bruge:
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
