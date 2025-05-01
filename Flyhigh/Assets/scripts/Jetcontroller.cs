using UnityEngine;
using System.Collections;

public class JetController : FlyingVehicle
{
    [Header("Specifikke Properties")] // gøre det nemmere at holde styr på 
    public float afterburnerMultiplier = 1.5f; // Denne variabel angiver, hvor meget jetmotorens kraft eller hastighed bliver forøget, når afterburneren aktiveres.
    public float afterburnerDuration = 2.0f;  // Denne variabel bestemmer, hvor længe afterburneren kan være aktiv, før den slukker automatisk.
    public float afterburnerCooldown = 5.0f; // Denne variabel angiver, hvor lang tid der skal gå, før afterburneren kan bruges igen, efter den har været aktiv.
    
    public GameObject exhaustImage;

    private bool afterburnerActive = false;
    private float afterburnerTimeRemaining = 0f;
    private float cooldownTimeRemaining = 0f;
    private Coroutine hideExhaustCoroutine;

    protected override void Awake()
    {
        base.Awake();
        gameObject.tag = "Jet";
    }

    private void Start()
    {
        speedDecayFactor = 0.997f;
        // Skjul billedet i starten
        if (exhaustImage != null)
            exhaustImage.SetActive(false);
    }

    // Nu bruger vi kun launchPower — ikke efterburnerMultiplier her
    public override void Launch(float launchPower)
    {
        base.Launch(launchPower);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        HandleAfterburner();
    }

    protected override void ProcessControlInput()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalSpeed = Mathf.Abs(rb.velocity.x);
        float controlFactor = Mathf.InverseLerp(minControlSpeed * 1.5f, minControlSpeed * 3, horizontalSpeed);
        rb.AddForce(Vector2.up * verticalInput * verticalControlMultiplier * controlFactor, ForceMode2D.Force);
    }

    private void HandleAfterburner()
    {
        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimeRemaining <= 0 && !afterburnerActive)
        {
            ActivateAfterburner();
        }

        if (afterburnerActive)
        {
            afterburnerTimeRemaining -= Time.fixedDeltaTime;
            if (afterburnerTimeRemaining <= 0)
                DeactivateAfterburner();
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
        cooldownTimeRemaining = afterburnerCooldown;

        // Boost hastighed
        float currentSpeed = rb.velocity.x;
        float boostedSpeed = currentSpeed * afterburnerMultiplier;
        if (Mathf.Abs(currentSpeed) < 5f)
            boostedSpeed += (currentSpeed >= 0 ? 5f : -5f);
        rb.velocity = new Vector2(boostedSpeed, rb.velocity.y);

        // Vis billedet
        if (exhaustImage != null)
        {
            exhaustImage.SetActive(true);
            if (hideExhaustCoroutine != null)
                StopCoroutine(hideExhaustCoroutine);
            hideExhaustCoroutine = StartCoroutine(HideExhaustImage(afterburnerDuration));
        }
    }

    private void DeactivateAfterburner()
    {
        afterburnerActive = false;
        if (exhaustImage != null)
            exhaustImage.SetActive(false);
        cooldownTimeRemaining = afterburnerCooldown;
    }

    private IEnumerator HideExhaustImage(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (exhaustImage != null)
            exhaustImage.SetActive(false);
        hideExhaustCoroutine = null;
    }

    public override void AddSpeed(float boostAmount)
    {
        rb.velocity = new Vector2(rb.velocity.x + boostAmount, rb.velocity.y);
    }
}
