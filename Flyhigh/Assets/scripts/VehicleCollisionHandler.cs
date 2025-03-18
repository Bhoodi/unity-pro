using UnityEngine;

public class VehicleCollisionHandler : MonoBehaviour
{
    // Reference to Game Over panel in a separate Canvas
    public GameObject gameOverPanel;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the vehicle hits water, destroy it
        if (collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("Collision with Water");
            GameOver();
        }
        // If the vehicle hits a bird, reduce speed
        else if (collision.gameObject.CompareTag("Bird"))
        {
            Debug.Log("Collision with Bird");
            FlyingVehicle vehicle = GetComponent<FlyingVehicle>();
            if (vehicle != null)
            {
                // Reduce horizontal speed by 2 units
                vehicle.AddSpeed(-2f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            Debug.Log("Trigger collision with Water");
            GameOver();
        }
        else if (other.gameObject.CompareTag("Bird"))
        {
            Debug.Log("Trigger collision with Bird");
            FlyingVehicle vehicle = GetComponent<FlyingVehicle>();
            if (vehicle != null)
            {
                vehicle.AddSpeed(-2f);
            }
        }
    }

    void GameOver()
    {
        // Activate Game Over panel in a separate Canvas
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            gameOverPanel.transform.SetAsLastSibling(); // Ensure panel appears on top
            Debug.Log("Game Over panel activated.");
        }
        
        // Delay destruction of the vehicle to allow UI to appear
        Invoke("DestroyVehicle", 0.5f);
    }

    void DestroyVehicle()
    {
        Destroy(gameObject);
    }
}
