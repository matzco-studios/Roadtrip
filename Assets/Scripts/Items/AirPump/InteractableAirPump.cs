using System.Collections;
using System.Collections.Generic;
using Car;
using Car.Parts;
using UnityEngine;

namespace Items.AirPump
{
    public class InteractableAirPump : Mechanics.Interactable
    {
        [SerializeField] private UI.ActionMessageController _message;
        private AirPumpItem _airPumpItem;
        private Animation _airPumpAnim;
        private CarController _car;

        void Start()
        {
            _airPumpItem = transform.parent.GetComponent<AirPumpItem>();
            _airPumpAnim = _airPumpItem.GetComponent<Animation>();
            _message = GetMessage();
            _car = GetCar();
        }

        public Wheel GetCurrentWheel() {
            if (!_airPumpItem.IsConnected) return null;
            WheelCollider wheel = _airPumpItem.IsConnected.GetComponent<WheelCollider>();
            Wheel wheelResult = null;
            _car.wheels.ForEach(w =>
            {
                if (w.wheelCollider == wheel)
                {
                    wheelResult = w;
                }
            });

            return wheelResult;
        }

        public override void InteractionMessage()
        {
            _message.InteractableItem("air", "to pump");
        }

        public override void OnInteract()
        {
            if (_airPumpAnim.isPlaying)
            {
                return;
            }

            if (_airPumpItem.IsConnected)
            {
                Wheel w = GetCurrentWheel();
                float pressure = Mathf.Max(1.15f, Mathf.Sqrt(Mathf.Max(30f-w.Pressure, 0))/2);
                w.AddPressure(pressure);
                print(pressure);
            }
            else
                print("Pump air for nothing");

            _airPumpAnim.Play();
        }
    }
}