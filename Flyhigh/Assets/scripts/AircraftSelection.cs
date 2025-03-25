using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AircraftSelection : MonoBehaviour
{
    [System.Serializable]
    public class Aircraft
    {
        public string name;
        public GameObject prefab;
        public Sprite thumbnail;
        [TextArea]
        public string description;
        public string gameSceneName; // Scene name for this aircraft
    }

    [Header("Aircraft Options")]
    [SerializeField] private List<Aircraft> availableAircraft = new List<Aircraft>();
    
    [Header("UI References")]
    [SerializeField] private Transform aircraftDisplayParent;
    [SerializeField] private GameObject aircraftButtonPrefab;
    [SerializeField] private Text aircraftNameText;
    [SerializeField] private Text aircraftDescriptionText;
    [SerializeField] private Image aircraftPreviewImage;
    [SerializeField] private Button playButton; // This will be optional now
    
    [Header("Navigation")]
    [SerializeField] private Button backButton;
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    
    private int selectedAircraftIndex = -1;
    private static readonly string SELECTED_AIRCRAFT_KEY = "SelectedAircraft";
    
    private void Start()
    {
        PopulateAircraftOptions();
        
        // Hide or disable the play button since we'll start directly when clicking aircraft
        if (playButton != null)
        {
            playButton.gameObject.SetActive(false);
        }
        
        // Set up back button
        if (backButton != null)
        {
            backButton.onClick.AddListener(ReturnToMainMenu);
        }
    }
    
    private void PopulateAircraftOptions()
    {
        // Clear any existing children
        foreach (Transform child in aircraftDisplayParent)
        {
            Destroy(child.gameObject);
        }
        
        // Create a button for each aircraft
        for (int i = 0; i < availableAircraft.Count; i++)
        {
            Aircraft aircraft = availableAircraft[i];
            GameObject buttonObj = Instantiate(aircraftButtonPrefab, aircraftDisplayParent);
            Button button = buttonObj.GetComponent<Button>();
            
            // Set the thumbnail if available
            Image buttonImage = buttonObj.GetComponent<Image>();
            if (buttonImage != null && aircraft.thumbnail != null)
            {
                buttonImage.sprite = aircraft.thumbnail;
            }
            
            // Add text component with aircraft name if needed
            Text buttonText = buttonObj.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = aircraft.name;
            }
            
            // Add the click listener to directly start the game
            int index = i; // Store index in local variable to use in lambda
            button.onClick.AddListener(() => {
                SelectAndStartGame(index);
            });
        }
    }
    
    private void SelectAndStartGame(int index)
    {
        if (index < 0 || index >= availableAircraft.Count)
            return;
            
        selectedAircraftIndex = index;
        
        // Save the selected aircraft index
        PlayerPrefs.SetInt(SELECTED_AIRCRAFT_KEY, selectedAircraftIndex);
        PlayerPrefs.Save();
        
        // Load the specific game scene for this aircraft
        SceneManager.LoadScene(availableAircraft[index].gameSceneName);
    }
    
    // This method can still be used by the separate play button if needed
    public void StartGame()
    {
        if (selectedAircraftIndex >= 0)
        {
            // Save the selected aircraft index
            PlayerPrefs.SetInt(SELECTED_AIRCRAFT_KEY, selectedAircraftIndex);
            PlayerPrefs.Save();
            
            // Load the specific game scene for this aircraft
            SceneManager.LoadScene(availableAircraft[selectedAircraftIndex].gameSceneName);
        }
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
    
    // This method can be called from other scripts to get the selected aircraft
    public static int GetSelectedAircraftIndex()
    {
        return PlayerPrefs.GetInt(SELECTED_AIRCRAFT_KEY, 0);
    }
    
    // This updated method is simpler since we know which aircraft to spawn in each scene
    public static GameObject SpawnAircraftInScene(Transform spawnPoint)
    {
        int selectedIndex = GetSelectedAircraftIndex();
        
        // Create a prefab reference based on the aircraft index
        GameObject prefabToSpawn;
        
        // This matches the aircraft to its specific scene
        switch (selectedIndex)
        {
            case 0:
                prefabToSpawn = Resources.Load<GameObject>("Aircraft/Aircraft1Prefab");
                break;
            case 1:
                prefabToSpawn = Resources.Load<GameObject>("Aircraft/Aircraft2Prefab");
                break;
            case 2:
                prefabToSpawn = Resources.Load<GameObject>("Aircraft/Aircraft3Prefab");
                break;
            default:
                prefabToSpawn = Resources.Load<GameObject>("Aircraft/DefaultAircraftPrefab");
                break;
        }
            
        // Instantiate the selected aircraft at the spawn point
        return Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
