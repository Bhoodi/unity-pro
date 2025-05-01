using UnityEngine;

public class FlyingVehicle : MonoBehaviour
{
    [Header("Base Properties")]
    public float startSpeed = 0f;                       // Bestemmer startfarten for flyet, når det begynder at bevæge sig.
    public float gravityScaleOverride = 0.8f;           // Justerer, hvor meget tyngdekraften påvirker flyet (lavere værdi = mindre påvirkning).
    public float speedDecayFactor = 0.995f;             // Reducerer farten gradvist over tid (en slags friktion i luften).
    public float verticalControlMultiplier = 5f;        // Forstærker, hvor hurtigt flyet kan bevæge sig opad eller nedad, når spilleren styrer det.
    public float minControlSpeed = 2f;                  // Angiver den minimale fart, hvor spilleren stadig kan kontrollere flyet.
    public float baseUpwardImpulse = 2f;                // Bestemmer den grundlæggende kraft, der skubber flyet opad (fx ved start eller hop).

    [HideInInspector] public Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScaleOverride;
    }

    public virtual void Launch(float launchPower)
    {
        startSpeed = launchPower;
        
        rb.velocity = new Vector2(startSpeed, baseUpwardImpulse);
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
    }
}
