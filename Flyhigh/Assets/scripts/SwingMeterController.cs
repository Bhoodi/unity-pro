using UnityEngine;
using UnityEngine.UI;

public class SwingMeterController : MonoBehaviour
{
    public Slider slider;
    public float minValue = 0f;
    public float maxValue = 10f;
    public float speed = 2f;
    // Forsinkelsen (i sekunder) inden swingmeteret skjules
    public float hideDelay = 1f;

    private bool goingUp = true;
    private bool locked = false;

    private FlyingVehicle flyingVehicle;

    void Start()
    {
        // Find det flyvende køretøj i scenen
        flyingVehicle = FindObjectOfType<FlyingVehicle>();
        Debug.Log("SwingMeter: Found flyingVehicle = " + flyingVehicle);

        // Indstil sliderens værdier
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = (minValue + maxValue) / 2f; // Starter i midten
    }

    void Update()
    {
        if (!locked)
        {
            // Få slideren til at svinge
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

            // Når Space trykkes, lås værdien og kald Launch()
            if (Input.GetKeyDown(KeyCode.Space))
            {
                locked = true;
                Debug.Log("SwingMeter: Kalder at value: " + slider.value);

                if (flyingVehicle != null)
                {
                    Debug.Log("SwingMeter: Kalder Launch with " + slider.value);
                    flyingVehicle.Launch(slider.value);
                }
                else
                {
                    Debug.LogWarning("SwingMeter: flyingVehicle is null! Launch not called.");
                }
                
                // Skjul swingmeteret efter en kort forsinkelse
                Invoke("HideSwingMeter", hideDelay);
            }
        }
    }

    void HideSwingMeter()
    {
        slider.gameObject.SetActive(false);
    }
}
