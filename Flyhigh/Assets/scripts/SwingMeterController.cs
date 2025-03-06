using UnityEngine;
using UnityEngine.UI;

public class SwingMeterController : MonoBehaviour
{
    public Slider slider;
    public float minValue = 0f;
    public float maxValue = 10f;
    public float speed = 2f;

    private bool goingUp = true;
    private bool locked = false;

    private PlaneController planeController;

    void Start()
    {
        // Find plane i scenen
        planeController = FindObjectOfType<PlaneController>();
        Debug.Log("SwingMeter: Found planeController = " + planeController);

        // Indstil sliderens værdier
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = (minValue + maxValue) / 2f; // start i midten
    }

    void Update()
    {
        // Log for at se om Update faktisk kører
        Debug.Log("SwingMeter Update kører");

        if (!locked)
        {
            // Sving op eller ned
            if (goingUp)
            {
                slider.value += speed * Time.deltaTime;
                if (slider.value >= maxValue)
                {
                    goingUp = false;
                }
            }
            else
            {
                slider.value -= speed * Time.deltaTime;
                if (slider.value <= minValue)
                {
                    goingUp = true;
                }
            }

            // Tjek om space bliver trykket
            if (Input.GetKeyDown(KeyCode.Space))
            {
                locked = true;
                Debug.Log("SwingMeter: locked at value: " + slider.value);

                // Kald Launch() hvis planeController ikke er null
                if (planeController != null)
                {
                    Debug.Log("SwingMeter: Calling Launch with " + slider.value);
                    planeController.Launch(slider.value);
                }
                else
                {
                    Debug.LogWarning("SwingMeter: planeController is null! Launch not called.");
                }
            }
        }
    }
}
