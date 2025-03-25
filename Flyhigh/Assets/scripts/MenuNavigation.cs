using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    // Navnet på den scene, hvor spilleren vælger fly – nu sat til "PlaneSelect"
    public string aircraftSelectionScene = "Plane select";

    // Denne metode kaldes, når knappen trykkes
    public void GoToAircraftSelectionMenu()
    {
        SceneManager.LoadScene(aircraftSelectionScene);
    }
}
