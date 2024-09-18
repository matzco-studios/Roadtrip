using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public float acceleration = 15f;
    private float deceleration = 20f;
    private float turnAngle = 15f;
    private float speedMultiplier;
    public float maxSpeed = 20f;

    private float gasInput;
    private float turnInput;
    private KeyCode startEngineKey = KeyCode.I;
    public float lowVolume = 0.25f;
    public float highVolume = 1f;
    public float minSpeedVolume = 10f;
    public float maxSpeedVolume = 21f;

    public bool IsRunning = false;
    void StartEngine()
    {
        if (Input.GetKeyDown(startEngineKey) && !carStarted)
        {
            engineStartSound.Play();
            engineSound.PlayDelayed(1f);
            engineSound.volume = 1f;
            carStarted = true;
        }
        else if (Input.GetKeyDown(startEngineKey) && carStarted)
        {
            carStarted = false;
            engineSound.pitch = currentEngineVolume;
        }
    }
    void GetInputs()
    {
        gasInput = (IsRunning) ? Input.GetAxis("Vertical") : 0.0f;
        turnInput =(IsRunning) ? Input.GetAxis("Horizontal") : 0.0f;
    }
    void MoveCarForward()
    {
        if (carStarted)
        {
            {
                foreach (Wheel wheel in wheels)
                {
                    wheel.wheelCollider.motorTorque = gasInput * acceleration * speedMultiplier * Time.deltaTime;
                }
            }
        }
        else return;
    }
    void TurnCar()
    {
        foreach (Wheel wheel in wheels)
        {
            if (wheel.isFrontWheel && currentSpeed > 1)
            {
                var _steerAngle = turnInput * turnAngle * (maxSpeed/currentSpeed);
                Debug.Log(_steerAngle);
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle/4, acceleration);
            }
        }
    }
    void Brake()
    {
        if (gasInput < 0) // if reverse/break is pressed
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = deceleration * 1000 * Time.deltaTime;
                wheel.wheelCollider.motorTorque = 0;
            }
        }
        else if (gasInput > 0 && carStarted) // if gas is pressed then it removes the brake
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
        else // if no gas is pressed then slows down the car
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = deceleration * 350 * Time.deltaTime;
                wheel.wheelCollider.motorTorque = 0;
            }
        }
    }
    void SetMaxSpeed() // sets the max speed of the car so it doesn't go faster and faster
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
        steeringWheel.transform.localRotation = Quaternion.Lerp(steeringWheel.transform.localRotation, Quaternion.AngleAxis(turnInput * -turnAngle * 3f, Vector3.forward), Time.deltaTime * 5f);
    }
    void EngineSound()
    {
        if (currentSpeed < minSpeedVolume)
        {
            engineSound.pitch = 0.25f;
        }
        if (currentSpeed > minSpeedVolume && currentSpeed < maxSpeedVolume)
        {
            engineSound.pitch = 0.25f + currentEngineVolume;
        }
        if (engineSound.pitch <= 0.25f && !carStarted)
        {
            engineSound.volume = 0;
        }
        
        engineSound.mute = !IsRunning;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        StartEngine();
        currentSpeed = rb.velocity.magnitude;
        currentEngineVolume = rb.velocity.magnitude / 50f;
    }
    void FixedUpdate()
    {
        MoveCarForward();
        Brake();
        AnimateWheels();
        AnimateSteeringWheel();
        TurnCar();
        SetMaxSpeed();
        EngineSound();
    }
}