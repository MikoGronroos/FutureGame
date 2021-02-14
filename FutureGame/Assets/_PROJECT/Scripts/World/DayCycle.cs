using UnityEngine;
using UnityEngine.Rendering;

public class DayCycle : MonoBehaviour
{

    [Range(0, 24)]
    [SerializeField] private float timeOfDay;

    [SerializeField] private float orbitSpeed = 1.0f;
    [SerializeField] private Light sun;
    [SerializeField] private Light moon;

    private bool isNight;

    private void OnValidate()
    {
        UpdateTime();
    }

    private void Update()
    {
        timeOfDay += Time.deltaTime * orbitSpeed;
        if (timeOfDay >= 24)
        {
            timeOfDay = 0;
        }

        UpdateTime();
    }

    private void UpdateTime()
    {
        float alpha = timeOfDay / 24.0f;
        float sunRotation = Mathf.Lerp(-90, 270, alpha);
        float moonRotation = sunRotation - 180;

        sun.transform.rotation = Quaternion.Euler(sunRotation, -50.0f, 0);
        moon.transform.rotation = Quaternion.Euler(moonRotation, -50.0f, 0);

        CheckNightDayTransition();
    }

    private void CheckNightDayTransition()
    {
        if (isNight)
        {
            if (moon.transform.rotation.eulerAngles.x > 180)
            {
                StartDay();
            }
        }
        else
        {
            if (sun.transform.rotation.eulerAngles.x > 180)
            {
                StartNight();
            }
        }
    }

    private void StartDay()
    {
        isNight = false;
    }

    private void StartNight()
    {
        isNight = true;
    }

}



