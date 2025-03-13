using UnityEngine;

public class PlaneController : FlyingVehicle
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        // OPTIONAL: Du kan teste en forced launch her, fx: Launch(10f);
    }

    public override void Launch(float lockedSpeed)
    {
        base.Launch(lockedSpeed);
        // Removed duplicate initialization of velocity since it's now in base class
        Debug.Log("Velocity after Launch: " + rb.velocity);
    }

    protected override void FixedUpdate()
    {
        Debug.Log("[PlaneController] FixedUpdate - Before speed decay, velocity: " + rb.velocity);
        base.FixedUpdate();
        Debug.Log("[PlaneController] FixedUpdate - After speed decay, velocity: " + rb.velocity);
    }
}
