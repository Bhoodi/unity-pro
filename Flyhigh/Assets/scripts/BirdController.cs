using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Fuglen bevæger sig fx fra højre mod venstre
        rb.velocity = new Vector2(-speed, 0f);
    }

    public void ReduceSpeed(float amount)
    {
        // Sæt farten ned, men ikke under 0
        speed -= amount;
        if (speed < 0f) speed = 0f;

        // Opdater fuglens velocity
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }
}
