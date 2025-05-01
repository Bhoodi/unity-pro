using UnityEngine;

public class PlaneController : FlyingVehicle
{
    [Header("Specifikke Properties")] // gøre det nemmere at holde styr på 
    public float bankingFactor = 0.5f;  // how much the plane tilts when turning
    public float aerodynamicEfficiency = 1.2f;  // multiply for horizontal speed

    protected override void Awake()
    {
        base.Awake();
        // Set tag for identification
        gameObject.tag = "Plane";
    }

    public override void Launch(float lockedSpeed)
    {
        base.Launch(lockedSpeed);
        // Apply aerodynamic boost
        rb.velocity = new Vector2(rb.velocity.x * aerodynamicEfficiency, rb.velocity.y);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        // Apply plane-specific banking (rotation based on vertical movement)
        float verticalInput = Input.GetAxis("Vertical");
        transform.rotation = Quaternion.Euler(0, 0, verticalInput * bankingFactor * rb.velocity.x);
        
    }
}
