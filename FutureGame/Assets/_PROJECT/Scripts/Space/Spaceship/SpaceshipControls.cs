using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipControls : MonoBehaviour
{

    [SerializeField] private float rocketForce;
    [SerializeField] private float lerpSmoothValue;

    private Rigidbody _rocketRb;
    private bool _rocketIsOn;

    private void Awake()
    {
        _rocketRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartEngines();
        }
        if (_rocketIsOn) AddForceToRocket();
    }

    private void StartEngines()
    {
        _rocketIsOn = true;
    }

    private void AddForceToRocket()
    {
        float previousVelocity = 0;
        float velocity = rocketForce * Time.time;
        Vector3 velocityVector = Vector3.Lerp(new Vector3(0, previousVelocity, 0), new Vector3(0, velocity, 0), lerpSmoothValue);
        _rocketRb.velocity = velocityVector * Time.deltaTime;
        previousVelocity = velocity;
    }
}
