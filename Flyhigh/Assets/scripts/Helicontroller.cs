using UnityEngine;

public class HelicopterController : FlyingVehicle
{
    [Header("Helicopter-Specific Properties")]
    public float hoverPower = 2.0f;
    public float maxHoverHeight = 10f;
    public float rotorRotationSpeed = 500f;
    
    private Transform rotorTransform;  // Assign this in the inspector
    
    protected override void Awake()
    {
        base.Awake();
        // Set tag for identification
        gameObject.tag = "Helicopter";
    }

    protected override void Start()
    {
        base.Start();
        
        // Find rotor if available (optional - can be set in inspector)
        if (rotorTransform == null)
        {
            Transform[] children = GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
            {
                if (child.name.ToLower().Contains("rotor"))
                {
                    rotorTransform = child;
                    break;
                }
            }
        }
    }

    public override void Launch(float launchPower)
    {
        // Helicopters have more vertical than horizontal movement
        base.Launch(launchPower * 0.8f);
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 1.5f);
    }

    protected override void FixedUpdate()
    {
        // Skip default speed decay but still process input
        ProcessControlInput();
        
        // Rotate rotor if found
        if (rotorTransform != null)
        {
            rotorTransform.Rotate(Vector3.forward, rotorRotationSpeed * Time.fixedDeltaTime);
        }
    }

    protected override void ProcessControlInput()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        
        // Allow helicopter to move horizontally with controls
        rb.velocity = new Vector2(horizontalInput * 3f, rb.velocity.y);
        
        // Apply hover force (stronger vertical control)
        rb.AddForce(Vector2.up * verticalInput * hoverPower, ForceMode2D.Force);
        
        // Limit max height
        if (transform.position.y > maxHoverHeight && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}
