using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float startSpeed = 0f;
    public float gravityScaleOverride = 0.8f;
    public float speedDecayFactor = 0.995f;
    public float verticalControlMultiplier = 5f;
    public float minControlSpeed = 2f;

    [HideInInspector] public Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScaleOverride;
        Debug.Log($"[PlaneController] Awake - gravityScale set to {gravityScaleOverride}");
    }

    void Start()
    {
        Debug.Log("[PlaneController] Start called");
        // OPTIONAL: Du kan teste en forced launch her, fx: Launch(10f);
    }

    public void Launch(float lockedSpeed)
    {
        startSpeed = lockedSpeed;
        // Tilføj en initial opadgående impuls for at give flyet lidt højde ved start.
        float initialUpwardImpulse = 2f; // Juster denne værdi efter behov.
        Debug.Log("Launch called with lockedSpeed: " + lockedSpeed);
        rb.velocity = new Vector2(startSpeed, initialUpwardImpulse);
        Debug.Log("Velocity after Launch: " + rb.velocity);
    }

    void FixedUpdate()
    {
        Debug.Log("[PlaneController] FixedUpdate - Before speed decay, velocity: " + rb.velocity);
        rb.velocity = new Vector2(rb.velocity.x * speedDecayFactor, rb.velocity.y);
        Debug.Log("[PlaneController] FixedUpdate - After speed decay, velocity: " + rb.velocity);

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalSpeed = Mathf.Abs(rb.velocity.x);
        float controlFactor = Mathf.InverseLerp(0, minControlSpeed, horizontalSpeed);
        Debug.Log("[PlaneController] VerticalInput: " + verticalInput + ", HorizontalSpeed: " + horizontalSpeed + ", ControlFactor: " + controlFactor);

        rb.AddForce(Vector2.up * verticalInput * verticalControlMultiplier * controlFactor, ForceMode2D.Force);
    }

    // Ny metode til at tilføje ekstra hastighed
    public void AddSpeed(float boostAmount)
    {
        rb.velocity = new Vector2(rb.velocity.x + boostAmount, rb.velocity.y);
        Debug.Log("AddSpeed called, new velocity: " + rb.velocity);
    }
}
