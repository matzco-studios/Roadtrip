using UnityEngine;

namespace Car.Parts
{
    public class InteractableKeyHole : Items.Mechanics.Interactable
    {
        [SerializeField] private UI.ActionMessageController _message;
        private CarController _car;

        void Start()
        {
            _message = GetMessage();
            _car = GetCar();
        }

        public override void InteractionMessage()
        {
            if (_car.IsPlayerInside)
            {
                _message.InteractableItem("car", "to turn " + (_car.IsCarRunning() ? "off" : "on"));
            }
        }

        public override void OnInteract()
        {
            if (_car.IsPlayerInside)
            {
                _car.StartEngine();
                InteractionMessage();
            }
        }
    }
}