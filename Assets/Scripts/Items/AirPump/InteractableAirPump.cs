using System.Collections;
using System.Collections.Generic;
using Car;
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
                WheelCollider wheel = _airPumpItem.IsConnected.GetComponent<WheelCollider>();
                _car.wheels.ForEach(w =>
                {
                    if (w.wheelCollider == wheel)
                    {
                        w.AddPressure(1.15f);
                    }

                    print(w.Pressure);
                });
            }
            else
                print("Pump air for nothing");

            _airPumpAnim.Play();
        }
    }
}