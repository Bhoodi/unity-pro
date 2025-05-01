using UnityEngine;

public class WindBooster : MonoBehaviour
{
    public float boostAmount = 5f;  // Hvor meget ekstra fart flyet får
    private bool destroyOnUse = true; // Wind-objektet skal forsvinde

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Tjekker om objektet har en FlyingVehicle-komponent
        FlyingVehicle vehicle = other.GetComponent<FlyingVehicle>();
        if (vehicle != null)
        {
            // Kald metoden på flyet, der øger farten
            vehicle.AddSpeed(boostAmount);
            
            if (destroyOnUse)
            {
                Destroy(gameObject);
            }
        }
    }
}
