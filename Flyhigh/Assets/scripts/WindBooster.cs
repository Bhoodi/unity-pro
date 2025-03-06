using UnityEngine;

public class WindBooster : MonoBehaviour
{
    [Header("Boost Settings")]
    public float boostAmount = 5f;  // Hvor meget ekstra fart flyet får
    public bool destroyOnUse = true; // Om wind-objektet skal forsvinde, når det er brugt

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Tjekker om objektet har en PlaneController
        PlaneController plane = other.GetComponent<PlaneController>();
        if (plane != null)
        {
            // Kald en metode på flyet, der øger farten
            plane.AddSpeed(boostAmount);

            // Hvis du vil have, at vind-objektet kun kan bruges én gang
            if (destroyOnUse)
            {
                Destroy(gameObject);
            }
        }
    }
}
