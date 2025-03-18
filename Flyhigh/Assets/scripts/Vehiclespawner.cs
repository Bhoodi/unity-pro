using UnityEngine;
using UnityEngine.UI;

public class VehicleSpawner : MonoBehaviour
{
    [Header("Vehicle Prefabs")]
    public GameObject planePrefab;
    public GameObject jetPrefab;
    public GameObject helicopterPrefab;
    
    [Header("Spawn Settings")]
    public Transform spawnPoint;
    public float initialLaunchPower = 10f;
    
    private GameObject currentVehicle;
    
    public void SpawnPlane()
    {
        SpawnVehicle(planePrefab);
    }
    
    public void SpawnJet()
    {
        SpawnVehicle(jetPrefab);
    }
    
    public void SpawnHelicopter()
    {
        SpawnVehicle(helicopterPrefab);
    }
    
    private void SpawnVehicle(GameObject vehiclePrefab)
    {
        // Remove existing vehicle if there is one
        if (currentVehicle != null)
        {
            Destroy(currentVehicle);
        }
        
        // Spawn the new vehicle
        currentVehicle = Instantiate(vehiclePrefab, spawnPoint.position, spawnPoint.rotation);
        
        // Launch the vehicle
        FlyingVehicle vehicleController = currentVehicle.GetComponent<FlyingVehicle>();
        if (vehicleController != null)
        {
            vehicleController.Launch(initialLaunchPower);
        }
    }
}
