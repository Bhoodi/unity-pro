using UnityEngine;

public class JetController : FlyingVehicle
{
    [Header("Jet-Specific Properties")]
    public float afterburnerMultiplier = 1.5f;
    public float afterburnerDuration = 2.0f;
    public float afterburnerCooldown = 5.0f;
    public ParticleSystem jetExhaustEffect;
    
    private bool afterburnerActive = false;
    private float afterburnerTimeRemaining = 0f;
    private float cooldownTimeRemaining = 0f;
    
    protected override void Awake()
    {
        base.Awake();
        // Set tag for identification
        gameObject.tag = "Jet";
    }

    protected override void Start()
    {
        base.Start();
        // Jets start with a higher base speed
        speedDecayFactor = 0.998f;  // Lower decay for jets
    }

    public override void Launch(float launchPower)
    {
        // Jets launch much faster
        base.Launch(launchPower * 1.5f);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        // Handle afterburner functionality
        HandleAfterburner();
    }

    protected override void ProcessControlInput()
    {
        // Jets require more speed to get the same level of control
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalSpeed = Mathf.Abs(rb.velocity.x);
        float controlFactor = Mathf.InverseLerp(minControlSpeed * 1.5f, minControlSpeed * 3, horizontalSpeed);
        
        rb.AddForce(Vector2.up * verticalInput * verticalControlMultiplier * controlFactor, ForceMode2D.Force);
    }

    private void HandleAfterburner()
    {
        // Activate afterburner with space key if not cooling down and not already active
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimeRemaining <= 0 && !afterburnerActive)
        {
            ActivateAfterburner();
        }
        
        // Update afterburner state
        if (afterburnerActive)
        {
            afterburnerTimeRemaining -= Time.fixedDeltaTime;
            if (afterburnerTimeRemaining <= 0)
            {
                DeactivateAfterburner();
            }
        }
        else if (cooldownTimeRemaining > 0)
        {
            cooldownTimeRemaining -= Time.fixedDeltaTime;
        }
    }
    
    private void ActivateAfterburner()
    {
        afterburnerActive = true;
        afterburnerTimeRemaining = afterburnerDuration;
        
        float currentSpeed = rb.velocity.x;
        // Multiplicative boost
        float boostedSpeed = currentSpeed * afterburnerMultiplier;
        
        // Hvis currentSpeed er lav, tilføj en fast boost
        float speedThreshold = 5f;  // Tærskel for lav hastighed
        float fixedBoost = 5f;      // Den faste boostmængde
        if (Mathf.Abs(currentSpeed) < speedThreshold)
        {
            if (currentSpeed >= 0)
                boostedSpeed += fixedBoost;
            else
                boostedSpeed -= fixedBoost;
        }
        
        rb.velocity = new Vector2(boostedSpeed, rb.velocity.y);
        
        if (jetExhaustEffect != null)
        {
            jetExhaustEffect.Play();
        }
        
        Debug.Log("[JetController] Afterburner activated! Boosted speed: " + boostedSpeed);
    }
    
    private void DeactivateAfterburner()
    {
        afterburnerActive = false;
        cooldownTimeRemaining = afterburnerCooldown;
        
        if (jetExhaustEffect != null)
        {
            jetExhaustEffect.Stop();
        }
        
        Debug.Log("[JetController] Afterburner deactivated, cooling down.");
    }
    
    public override void AddSpeed(float boostAmount)
    {
        rb.velocity = new Vector2(rb.velocity.x + boostAmount, rb.velocity.y);
        Debug.Log($"[{GetType().Name}] AddSpeed called, new velocity: {rb.velocity}");
    }
}
