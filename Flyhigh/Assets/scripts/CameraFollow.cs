using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target to follow")]
    public Transform target; // Det objekt (fx flyet), kameraet skal følge

    [Header("Follow settings")]
    public float smoothTime = 0.3f;   // Hvor “blødt” kameraet følger med
    private Vector3 velocity = Vector3.zero;

    [Header("Fixed camera position")]
    public float fixedY = 0f;        // Fast y-position, så vi ikke følger flyet op/ned
    public float offsetX = 2f;       // Hvor langt fremme kameraet er ift. flyet på x-aksen
    public float offsetZ = -10f;     // Typisk -10 i 2D (orthographic)

    void LateUpdate()
    {
        if (target == null) return;

        // Her vælger vi KUN at følge target’s x-position, 
        // men beholder en fast y (fixedY) og en fast z (offsetZ).
        Vector3 targetPosition = new Vector3(
            target.position.x + offsetX, 
            fixedY, 
            offsetZ
        );

        // SmoothDamp glider kameraet mod targetPosition over tid,
        // så det ikke rykker brat, men mere “smooth”.
        transform.position = Vector3.SmoothDamp(
            transform.position, 
            targetPosition, 
            ref velocity, 
            smoothTime
        );
    }
}
