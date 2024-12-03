using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Car.Parts;
using Items;

namespace Cinematic.EndScene
{
    public class CarScript : MonoBehaviour
    {
        private Rigidbody _rb;
        public List<Wheel> Wheels;
        private bool _running;
        public AudioSource engineSound;
        public AudioSource engineCoughSound;
        public AudioSource CrashSound;
        public List<CarLight> carLights;
        public float currentSpeed;
        private float currentEngineVolume;
        [SerializeField] private Animator _leftFrontDoorAnim;

        public float acceleration = 15f;
        public float BrakeDeceleration = 6500f;
        private float turnAngle = 15f;
        private float speedMultiplier;
        private float carWheelMaxAngle = 150f;
        public float maxSpeed = 15f;
        public bool IsLightsOn = true;
        private float gasInput = 5f;
        public float lowVolume = 0.25f;
        public float highVolume = 1f;
        public float minSpeedVolume = 10f;
        public float maxSpeedVolume = 21f;
        public bool IsPlayerInside;
        private bool _batteryDead;
        private PlayerScript _player;

        private bool _stopCarTrigger;

        private void PlayDeadBatterySound()
        {
            Debug.Log("The battery is dead.");
            engineCoughSound.volume = 0.5f;
            engineCoughSound.Play();
        }

        private void ToggleEngine()
        {
            if (!IsPlayerInside) return;

            if (_running)
            {
                _running = false;
                engineSound.pitch = currentEngineVolume;
            }

            else if (!_batteryDead)
            {
                engineSound.Play();
                engineSound.volume = 1f;
                _running = true;
            }
        }

        private void MoveCarForward()
        {
            foreach (var wheel in Wheels)
            {
                wheel.wheelCollider.motorTorque = gasInput * acceleration * speedMultiplier * Time.deltaTime;
            }
        }

        private void SetMaxSpeed() => // sets the max speed of the car so it doesn't go faster and faster
            speedMultiplier = currentSpeed > maxSpeed ? 0 : 100;

        private void AnimateWheels()
        {
            foreach (Wheel wheel in Wheels)
            {
                wheel.wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
                wheel.wheelObject.transform.position = position;
                wheel.wheelObject.transform.rotation = rotation;
            }
        }

        private void EngineSound()
        {
            if (currentSpeed < minSpeedVolume) engineSound.pitch = 0.25f;
            if (currentSpeed > minSpeedVolume && currentSpeed < maxSpeedVolume)
                engineSound.pitch = 0.25f + currentEngineVolume;
            if (engineSound.pitch <= 0.25f && !_running) engineSound.volume = 0;

            engineSound.mute = !IsPlayerInside && !_running;
        }

        private void ToggleLights()
        {
            foreach (var currentLight in carLights)
            {
                currentLight.ULight.intensity = IsLightsOn ? 0 : 1;
                if (IsLightsOn) currentLight.UFlare.Stop();
                else currentLight.UFlare.Play();
            }

            IsLightsOn = !IsLightsOn;
        }

        IEnumerator DeadBatteryEvent()
        {
            StartCoroutine(Brake());

            _batteryDead = true;
            ToggleEngine();
            ToggleLights();

            yield return new WaitForSeconds(1f);
            engineSound.mute = true;

            for (int i = 0; i < 3; i++)
            {
                PlayDeadBatterySound();
                yield return new WaitForSeconds(2f);
            }

            currentSpeed = 0;

            _leftFrontDoorAnim.enabled = true;
            CrashSound.Play();

            yield return new WaitForSeconds(1f);
            StartCoroutine(_player.PlayerMovement());
        }

        private IEnumerator Brake()
        {

            yield return new WaitWhile(() =>
            {
                print($"Velocity: {_rb.velocity.magnitude}");

                foreach (var wheel in Wheels)
                {
                    wheel.wheelCollider.brakeTorque = BrakeDeceleration * Time.deltaTime;
                    wheel.wheelCollider.motorTorque = 0;
                }

                AnimateWheels();
                return _rb.velocity.magnitude > 0.8;
            });
        }

        private void Start()
        {

            _player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
            _rb = GetComponent<Rigidbody>();
            _rb.velocity.Set(4.65f, 0.00f, 5.55f);
            
            engineSound = GetComponent<AudioSource>();
            ToggleEngine();
            EngineSound();
            _leftFrontDoorAnim.enabled = false;

            StartCoroutine(CarLoop());
        }

        IEnumerator CarLoop()
        {
            while (!_batteryDead)
            {
                yield return null;

                currentSpeed = _rb.velocity.magnitude;
                currentEngineVolume = _rb.velocity.magnitude / 50f;

                MoveCarForward();
                AnimateWheels();
                SetMaxSpeed();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("EndTrigger") && !_stopCarTrigger)
            {
                _stopCarTrigger = true;
                StartCoroutine(DeadBatteryEvent());
            }
        }
    }
}