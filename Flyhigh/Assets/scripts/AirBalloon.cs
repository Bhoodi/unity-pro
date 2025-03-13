using UnityEngine;

public class AirBalloon : FlyingVehicle
{
    [Header("Air Balloon Properties")]
    public float backwardSpeedMultiplier = 0.5f;
    
    protected override void ProcessControlInput()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        float horizontalSpeed = Mathf.Abs(rb.velocity.x);
        float controlFactor = Mathf.InverseLerp(0, minControlSpeed, horizontalSpeed);
        
        // Apply vertical control as usual
        rb.AddForce(Vector2.up * verticalInput * verticalControlMultiplier * controlFactor, ForceMode2D.Force);
        
        // Add backward movement ability - unique to the air balloon
        if (horizontalInput < 0) // Left input
        {
            rb.AddForce(Vector2.left * backwardSpeedMultiplier, ForceMode2D.Force);
        }
    }
    
    // You could also override other methods as needed
    public override void Launch(float launchPower)
    {
        // Custom air balloon launch behavior
        base.Launch(launchPower * 0.7f); // Slower initial launch for balloons
        
        // Add balloon-specific launch effects
        Debug.Log("Air balloon is rising slowly!");
    }
}
