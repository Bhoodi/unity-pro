using UnityEngine;
using UnityEngine.UI;

public class DistanceUI : MonoBehaviour
{
    public Text distanceText; 
    private PlaneController plane;
    private GameObject platform;

    void Start()
    {
        plane = FindObjectOfType<PlaneController>();
        // Vi går ud fra at GameManager har spawnet en platform
        // men vi kan finde den ved:
        platform = FindObjectOfType<GameManager>().GetComponent<GameManager>().platformPrefab;
        // eller FindObjectOfType<LandingPlatform> hvis platform har et script.
    }

    void Update()
    {
        // Hvis platformen er instantiatet et andet sted, kan du hente en reference fra GameManager:
        platform = GameObject.FindGameObjectWithTag("PlatformTag"); 
        // Husk at sætte "PlatformTag" på platformen i Unity, hvis du bruger FindGameObjectWithTag

        if (plane != null && platform != null)
        {
            float distance = Vector2.Distance(plane.transform.position, platform.transform.position);
            distanceText.text = "Distance: " + distance.ToString("F2");
        }
    }
}
