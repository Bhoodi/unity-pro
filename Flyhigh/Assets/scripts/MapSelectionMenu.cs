using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectionMenu : MonoBehaviour
{
    // Navnene på de scener, du vil loade – sørg for, at de matcher dine scenenavne præcist
    public string helicopterMapScene = "HelicopterMap";
    public string planeMapScene = "PlaneMap";
    public string jetMapScene = "JetMap";

    // Metode til at loade HelicopterMap
    public void LoadHelicopterMap()
    {
        SceneManager.LoadScene(helicopterMapScene);
    }

    // Metode til at loade PlaneMap
    public void LoadPlaneMap()
    {
        SceneManager.LoadScene(planeMapScene);
    }
    
    // Metode til at loade JetMap
    public void LoadJetMap()
    {
        SceneManager.LoadScene(jetMapScene);
    }
}
