using UnityEngine;

public class Helicopter : FlyingVehicle
{
    [Header("Helicopter Specific Properties")]
    public float hoverForce = 9.8f;
    public float maxHoverHeight = 10f;
    public float rotorSpeed = 500f;
    
    private Transform rotorTransform;

    protected override void Awake()
    {
        base.Awake();
        rotorTransform = transform.Find("Rotor");
    }

    public override void Launch(float launchPower)
    {
        base.Launch(launchPower);
        // Helicopters launch more vertically
        rb.velocity = new Vector2(launchPower * 0.5f, launchPower * 0.8f);
    }

    protected override void ProcessControlInput()
    {
        // Override with helicopter-specific controls
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        
        // Apply hover force to counteract gravity
        rb.AddForce(Vector2.up * hoverForce * verticalInput, ForceMode2D.Force);
        
        // Apply horizontal movement
        rb.AddForce(Vector2.right * horizontalInput * verticalControlMultiplier, ForceMode2D.Force);
        
        // Rotate rotor if found
        if (rotorTransform != null)
        {
            rotorTransform.Rotate(0, 0, rotorSpeed * Time.deltaTime);
        }
    }

    protected override void ApplySpeedDecay()
    {
        // Helicopters have more air resistance
        rb.velocity = new Vector2(rb.velocity.x * (speedDecayFactor - 0.01f), rb.velocity.y);
        
        // Limit maximum height
        if (transform.position.y > maxHoverHeight)
        {
            Vector2 currentVelocity = rb.velocity;
            if (currentVelocity.y > 0)
            {
                rb.velocity = new Vector2(currentVelocity.x, 0);
            }
        }
    }
}
