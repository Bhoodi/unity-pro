using UnityEngine;

public class WindBooster : MonoBehaviour
{
    [Header("Boost Settings")]
    public float boostAmount = 5f;  // Hvor meget ekstra fart flyet får
    public bool destroyOnUse = true; // Om wind-objektet skal forsvinde, når det er brugt

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Tjekker om objektet har en FlyingVehicle-komponent
        FlyingVehicle vehicle = other.GetComponent<FlyingVehicle>();
        if (vehicle != null)
        {
            // Kald metoden på flyet, der øger farten
            vehicle.AddSpeed(boostAmount);

            // Hvis du vil have, at wind-objektet kun kan bruges én gang
            if (destroyOnUse)
            {
                Destroy(gameObject);
            }
        }
    }
}
