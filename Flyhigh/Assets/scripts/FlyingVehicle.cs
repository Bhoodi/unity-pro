using UnityEngine;

public class FlyingVehicle : MonoBehaviour
{
    [Header("Base Vehicle Properties")]
    public float startSpeed = 0f;
    public float gravityScaleOverride = 0.8f;
    public float speedDecayFactor = 0.995f;
    public float verticalControlMultiplier = 5f;
    public float minControlSpeed = 2f;
    public float baseUpwardImpulse = 2f;  // Renamed from initialUpwardImpulse to avoid serialization conflicts

    [HideInInspector] public Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScaleOverride;
        Debug.Log($"[{GetType().Name}] Awake - gravityScale set to {gravityScaleOverride}");
    }

    protected virtual void Start()
    {
        Debug.Log($"[{GetType().Name}] Start called");
    }

    public virtual void Launch(float launchPower)
    {
        startSpeed = launchPower;
        Debug.Log($"[{GetType().Name}] Launch called with power: {launchPower}");
        
        // Update to use the renamed field
        rb.velocity = new Vector2(startSpeed, baseUpwardImpulse);
        Debug.Log("Velocity after Launch: " + rb.velocity);
    }

    protected virtual void FixedUpdate()
    {
        ApplySpeedDecay();
        ProcessControlInput();
    }

    protected virtual void ApplySpeedDecay()
    {
        rb.velocity = new Vector2(rb.velocity.x * speedDecayFactor, rb.velocity.y);
    }

    protected virtual void ProcessControlInput()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalSpeed = Mathf.Abs(rb.velocity.x);
        float controlFactor = Mathf.InverseLerp(0, minControlSpeed, horizontalSpeed);
        
        rb.AddForce(Vector2.up * verticalInput * verticalControlMultiplier * controlFactor, ForceMode2D.Force);
    }

    public virtual void AddSpeed(float boostAmount)
    {
        rb.velocity = new Vector2(rb.velocity.x + boostAmount, rb.velocity.y);
        Debug.Log($"[{GetType().Name}] AddSpeed called, new velocity: {rb.velocity}");
    }
}
