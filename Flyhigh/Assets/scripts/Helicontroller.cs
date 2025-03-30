using UnityEngine;

public class HelicopterController : FlyingVehicle
{
    [Header("Helicopter-Specific Properties")]
    public float hoverPower = 2.0f;
    public float maxHoverHeight = 10f;
    public float rotorRotationSpeed = 500f;
    
    private Transform rotorTransform;  // Assign this in the Inspector

    protected override void Awake()
    {
        base.Awake();
        // Sæt tag for identifikation
        gameObject.tag = "Helicopter";
    }

    protected override void Start()
    {
        base.Start();
        
        // Find rotor hvis ikke allerede sat via Inspector
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
        // Helikoptere har mere fokus på vertikal bevægelse
        base.Launch(launchPower * 0.8f);
        // Forøg den vertikale hastighed en smule
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 1.5f);
    }

    protected override void FixedUpdate()
    {
        // Behandl brugerinput for helikopteren
        ProcessControlInput();
        
        // Roter rotor, hvis den findes
        if (rotorTransform != null)
        {
            rotorTransform.Rotate(Vector3.forward, rotorRotationSpeed * Time.fixedDeltaTime);
        }
    }

    protected override void ProcessControlInput()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        
        // I stedet for at sætte den vandrette hastighed direkte, tilføj en kraft baseret på input
        rb.AddForce(new Vector2(horizontalInput * 3f, 0f), ForceMode2D.Force);
        
        // Tilføj en hover-force for vertikalt input
        rb.AddForce(Vector2.up * verticalInput * hoverPower, ForceMode2D.Force);
        
        // Begræns maksimal højde: Hvis helikopteren overstiger maxHoverHeight, nulstil den vertikale hastighed
        if (transform.position.y > maxHoverHeight && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}
