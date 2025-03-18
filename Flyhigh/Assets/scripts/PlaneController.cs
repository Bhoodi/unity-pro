using UnityEngine;

public class PlaneController : FlyingVehicle
{
    [Header("Plane-Specific Properties")]
    public float bankingFactor = 0.5f;  // How much the plane tilts when turning
    public float aerodynamicEfficiency = 1.2f;  // Multiplier for horizontal speed

    protected override void Awake()
    {
        base.Awake();
        // Set tag for identification
        gameObject.tag = "Plane";
    }

    protected override void Start()
    {
        base.Start();
        // OPTIONAL: Du kan teste en forced launch her, fx: Launch(10f);
    }

    public override void Launch(float lockedSpeed)
    {
        base.Launch(lockedSpeed);
        // Apply aerodynamic boost
        rb.velocity = new Vector2(rb.velocity.x * aerodynamicEfficiency, rb.velocity.y);
        Debug.Log("Velocity after Launch: " + rb.velocity);
    }

    protected override void FixedUpdate()
    {
        Debug.Log("[PlaneController] FixedUpdate - Before speed decay, velocity: " + rb.velocity);
        base.FixedUpdate();
        
        // Apply plane-specific banking (rotation based on vertical movement)
        float verticalInput = Input.GetAxis("Vertical");
        transform.rotation = Quaternion.Euler(0, 0, verticalInput * bankingFactor * rb.velocity.x);
        
        Debug.Log("[PlaneController] FixedUpdate - After speed decay, velocity: " + rb.velocity);
    }
}
