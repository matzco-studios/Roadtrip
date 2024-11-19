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
        public List<CarLight> carLights;
        public float currentSpeed;
        private float currentEngineVolume;
        [SerializeField] private Animator _leftFrontDoorAnim;

        public float acceleration = 15f;
        private float deceleration = 10f;
        private float turnAngle = 15f;
        private float speedMultiplier;
        private float carWheelMaxAngle = 150f;
        public float maxSpeed = 20f;
        public bool IsLightsOn;
        private float gasInput = 5f;
        public float lowVolume = 0.25f;
        public float highVolume = 1f;
        public float minSpeedVolume = 10f;
        public float maxSpeedVolume = 21f;
        public bool IsPlayerInside;
        private bool _batteryDead;
        private bool _stopped = true;

        void PlayDeadBatterySound()
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
                _stopped = false;
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
            speedMultiplier = (currentSpeed > maxSpeed) ? 0 : 100;

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
                currentLight.ULight.intensity = IsLightsOn ? 1 : 0;
                if (IsLightsOn) currentLight.UFlare.Play();
                else currentLight.UFlare.Stop();
            }

            IsLightsOn = !IsLightsOn;
        }

        IEnumerator DeadBatteryEvent()
        {
            yield return new WaitForSeconds(3f);
            _batteryDead = true;
            ToggleEngine();
            ToggleLights();

            for (int i = 0; i < 3; i++)
            {
                PlayDeadBatterySound();
                yield return new WaitForSeconds(2f);
            }

            currentSpeed = 0;
            engineSound.Stop();
            _stopped = false;
        }

        private void Brake()
        {
            foreach (var wheel in Wheels)
            {
                wheel.wheelCollider.brakeTorque = deceleration * 350 * Time.deltaTime;
                wheel.wheelCollider.motorTorque = 0;
            }
        }

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            engineSound = GetComponent<AudioSource>();
            ToggleLights();
            ToggleEngine();
            EngineSound();
            _leftFrontDoorAnim.enabled = false;

            StartCoroutine(DeadBatteryEvent());
        }

        void Update()
        {
            if (!_stopped)
            {
                currentSpeed = _rb.velocity.magnitude;
                currentEngineVolume = _rb.velocity.magnitude / 50f;
            }
        }

        void FixedUpdate()
        {
            if (!_batteryDead)
            {
                MoveCarForward();
                AnimateWheels();
                SetMaxSpeed();
            }
            else if(!_stopped)
            {
                Brake();
            }
        }
    }
}