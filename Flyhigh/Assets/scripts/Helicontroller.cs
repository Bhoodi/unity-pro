using UnityEngine;

public class HelicopterController : FlyingVehicle
{
    [Header("Specefikke Properties")] // gøre det nemmere at holde styr på 
    public float hoverPower = 2.0f;  // Kraften, der bruges til at holde helikopteren svævende
    public float maxHoverHeight = 10f; // Den maksimale højde, helikopteren kan svæve over jorden
    public float rotorRotationSpeed = 500f; // Hastigheden, hvormed rotorbladene roterer (i grader pr. sekund)
    public float horizontalForce = 1.5f;   // Kraften, der bruges til at bevæge helikopteren horisontalt
    public float maxHorizontalSpeed = 5f; // Den maksimale hastighed, helikopteren kan bevæge sig horisontalt
    
    private Transform rotorTransform;  

    protected override void Awake()
    {
        base.Awake();
        // Sæt tag for identifikation
        gameObject.tag = "Helicopter";
    }

    protected void Start()
    {
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
        // Behandle brugerinput for helikopteren
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
        
        // Reduce the horizontal force multiplier
        rb.AddForce(new Vector2(horizontalInput * horizontalForce, 0f), ForceMode2D.Force);
        
        // Clamp horizontal velocity to limit max speed
        Vector2 velocity = rb.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
        rb.velocity = velocity;
        
        // Tilføj en hover-force for vertikalt input
        rb.AddForce(Vector2.up * verticalInput * hoverPower, ForceMode2D.Force);
        
        // Begræns maksimal højde: Hvis helikopteren overstiger maxHoverHeight, nulstil den vertikale hastighed
        if (transform.position.y > maxHoverHeight && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }
}
