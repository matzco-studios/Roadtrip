using System;
using System.Collections.Generic;
using System.Linq;
using Car.Parts;
using Items;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        private Rigidbody rb;
        public List<Wheel> wheels;
        public GameObject steeringWheel;
        private bool IsRunning = false;
        public AudioSource engineSound;
        public AudioSource engineStartSound;
        public AudioSource engineCoughSound;
        public List<CarLight> carLights;
        public float currentSpeed;
        private float currentEngineVolume;

        public float acceleration = 15f;
        private float deceleration = 20f;
        private float turnAngle = 15f;
        private float speedMultiplier;
        private float carWheelMaxAngle = 150f;
        public float maxSpeed = 20f;
        private bool outOfFuel;
        public float currentFuel;
        private float fuelConsumption;
        public bool IsLightsOn;
        public Image fuelBar;
        public Battery Battery;
        private float gasInput;
        private float turnInput;
        private KeyCode startEngineKey = KeyCode.I;
        public float lowVolume = 0.25f;
        public float highVolume = 1f;
        public float minSpeedVolume = 10f;
        public float maxSpeedVolume = 21f;
        public bool IsPlayerInside = false;
        public const float MaxFuel = 100f;

        public bool IsBatteryInside() => Battery != null;

        public bool IsCarRunning()
        {
            return IsRunning;
        }

        public void StartEngine()
        {

            if (IsPlayerInside)
            {
                if (!IsRunning)
                {
                    if (outOfFuel)
                    {
                        Debug.Log("Out of fuel");
                        engineCoughSound.volume = 0.5f;
                        engineCoughSound.Play();
                    }
                    else if (!IsBatteryInside() || Battery.IsDead())
                    {
                        Debug.Log(!IsBatteryInside() ? "The battery is not inside the car." : "The battery is dead.");
                        engineCoughSound.volume = 0.5f;
                        engineCoughSound.Play();
                    }
                    else
                    {
                        engineStartSound.Play();
                        engineSound.PlayDelayed(1f);
                        engineSound.volume = 1f;
                        IsRunning = true;
                    }
                }
                else if (IsRunning)
                {
                    IsRunning = false;
                    engineSound.pitch = currentEngineVolume;
                }
            }
        }

        private void GetInputs()
        {
            gasInput = IsPlayerInside ? Input.GetAxis("Vertical") : 0.0f;
            turnInput = IsPlayerInside ? Input.GetAxis("Horizontal") : 0.0f;
        }

        private void MoveCarForward()
        {
            if (IsRunning)
            {
                {
                    foreach (var wheel in wheels)
                    {
                        wheel.wheelCollider.motorTorque = gasInput * acceleration * speedMultiplier * Time.deltaTime;
                    }
                }
                fuelConsumption = currentSpeed * Math.Abs(gasInput);
            }
            else fuelConsumption = 0f;
        }

        public void Refuel(float amount)
        {
            if (currentFuel < 100)
            {
                currentFuel += amount;
                Math.Clamp(currentFuel, 0, 100);
                fuelBar.fillAmount = currentFuel / 100;
            }
        }

        // Wrong name, temporarily calling it like that.
        public void RemoveFuel(float amount)
        {
            currentFuel = Mathf.Clamp(currentFuel - amount, 0, MaxFuel);
        }
        
        [Obsolete]
        void ReduceFuel()
        {
            if (currentFuel <= 0)
            {
                outOfFuel = true;
                IsRunning = false;
                engineSound.pitch = currentEngineVolume;
            }
            else
            {
                outOfFuel = false;
                currentFuel -= Time.deltaTime * fuelConsumption;
                Math.Clamp(currentFuel, 0, 100);
                fuelBar.fillAmount = currentFuel / 100;
            }
        }

        private void TurnCar()
        {
            if (currentSpeed > 0.1f)
            {
                float _steerAngle = turnInput * turnAngle * (maxSpeed / currentSpeed);
                _steerAngle = Mathf.Clamp(_steerAngle, -carWheelMaxAngle, carWheelMaxAngle);
                foreach (Wheel wheel in wheels)
                {
                    if (wheel.isFrontWheel)
                    {
                        wheel.wheelCollider.steerAngle =
                            Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle / 4, acceleration);
                    }
                }
            }
        }

        private void Brake()
        {
            if (gasInput < 0) // if reverse/break is pressed
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = deceleration * 1000 * Time.deltaTime;
                    wheel.wheelCollider.motorTorque = 0;
                }
            
            // if gas is pressed then it removes the brake
            else if (gasInput > 0 && IsRunning) foreach (var wheel in wheels) wheel.wheelCollider.brakeTorque = 0;
            
            // if no gas is pressed then slows down the car
            else 
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = deceleration * 350 * Time.deltaTime;
                    wheel.wheelCollider.motorTorque = 0;
                }
        }

        private void SetMaxSpeed() => // sets the max speed of the car so it doesn't go faster and faster
            speedMultiplier = (currentSpeed > maxSpeed) ? 0 : 400; 

        private void AnimateWheels()
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
                wheel.wheelObject.transform.position = position;
                wheel.wheelObject.transform.rotation = rotation;
            }
        }

        private void AnimateSteeringWheel() =>
            steeringWheel.transform.localRotation = Quaternion.Lerp(steeringWheel.transform.localRotation,
                Quaternion.AngleAxis(turnInput * -turnAngle * 3f, Vector3.forward), Time.deltaTime * 5f);

        private void EngineSound()
        {
            if (currentSpeed < minSpeedVolume) engineSound.pitch = 0.25f;
            if (currentSpeed > minSpeedVolume && currentSpeed < maxSpeedVolume) engineSound.pitch = 0.25f + currentEngineVolume;
            if (engineSound.pitch <= 0.25f && !IsRunning) engineSound.volume = 0;
            
            engineSound.mute = !IsPlayerInside && !IsRunning;
        }

        private void DisableLights()
        {
            foreach (var light in carLights.Where(light => !light.IsWorking))
            {
                light.ULight.intensity = 0;
                light.UFlare.Stop();
            } 
        }
        
        private void ToggleLights()
        {
            if (Input.GetKeyDown(KeyCode.L) && IsPlayerInside)
            {
                IsLightsOn = !IsLightsOn;
                foreach (var light in carLights)
                {
                    if (light.IsWorking) light.ULight.intensity = IsLightsOn ? 1 : 0;
                    if (IsLightsOn) light.UFlare.Play(); else light.UFlare.Stop();
                }
            }
            DisableLights();      
        }

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            engineSound = GetComponent<AudioSource>();
            currentFuel = 50f;
            fuelBar.fillAmount = 1f;
        }

        // Update is called once per frame
        void Update()
        {
            GetInputs();
            ToggleLights();
            ReduceFuel();
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
}