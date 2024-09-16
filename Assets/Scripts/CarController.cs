using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Serializable]
    public struct Wheel
    {
        public GameObject wheelObject;
        public WheelCollider wheelCollider;
        public bool isFrontWheel;
    }
    private Rigidbody rb;
    public List<Wheel> wheels;
    public GameObject steeringWheel;
    private bool carStarted = false;
    public AudioSource engineSound;
    public AudioSource engineStartSound;

    private float currentSpeed;
    private float currentEngineVolume;

    public float acceleration = 25f;
    private float deceleration = 20f;
    private float turnAngle = 10f;
    private float speedMultiplier;
    public float maxSpeed = 20f;

    private float gasInput;
    private float turnInput;
    private KeyCode startEngine = KeyCode.I;
    public float lowVolume = 0.1f;
    public float highVolume = 1f;
    public float minSpeedVolume = 1f;
    public float maxSpeedVolume = 21f;

    void StartEngine()
    {
        if (Input.GetKeyDown(startEngine) && !carStarted)
        {
            engineStartSound.Play();
            engineSound.PlayDelayed(1f);
            carStarted = true;
        } else if (Input.GetKeyDown(startEngine) && carStarted)
        {
            engineSound.Stop();
            carStarted = false;
        }
    }
    void getInputs()
    {
        gasInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }
    void MoveCarForward()
    {
        if (!carStarted) return;
        else
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = gasInput * acceleration * speedMultiplier * Time.deltaTime;
            }
        }
    }
    void TurnCar()
    {
        foreach (Wheel wheel in wheels)
        {
            if (wheel.isFrontWheel)
            {
                var _steerAngle = turnInput * turnAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, acceleration);
            }
        }
    }
    void Brake()
    {
        if (gasInput == 0) // if no gas is pressed then slows down the car
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = deceleration * 500 * Time.deltaTime;
            }
        }
        else if (gasInput < 0) // if reverse/break is pressed
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = deceleration * 1000 * Time.deltaTime;
            }
        }
        else // if gas is pressed then it removes the brake
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }
    void setMaxSpeed() // sets the max speed of the car so it doesn't go faster and faster
    {
        if (currentSpeed > maxSpeed)
        {
            speedMultiplier = 0;
        }
        else
        {
            speedMultiplier = 400;
        }
    }
    void AnimateWheels()
    {
        foreach (Wheel wheel in wheels)
        {
            wheel.wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
            wheel.wheelObject.transform.position = position;
            wheel.wheelObject.transform.rotation = rotation;
        }
    }
    void AnimateSteeringWheel()
    {
        steeringWheel.transform.localRotation = Quaternion.Lerp(steeringWheel.transform.localRotation, Quaternion.AngleAxis(turnInput * -turnAngle * 5f, Vector3.forward), Time.deltaTime * 5f);
    }
    void EngineSound()
    {
        currentEngineVolume = rb.velocity.magnitude / 50f;
        if (currentSpeed < minSpeedVolume)
        {
            engineSound.pitch = lowVolume;
        }
        if (currentSpeed > minSpeedVolume && currentSpeed < maxSpeedVolume)
        {
            engineSound.pitch = lowVolume + currentEngineVolume;
        }
        if (currentSpeed > maxSpeedVolume)
        {
            engineSound.pitch = highVolume;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.9f, 0);
        engineSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        getInputs();
        StartEngine();
        currentSpeed = rb.velocity.magnitude;
    }
    void FixedUpdate()
    {
        MoveCarForward();
        Brake();
        AnimateWheels();
        AnimateSteeringWheel();
        TurnCar();
        setMaxSpeed();
        EngineSound();
    }
}