using UnityEngine;

namespace Car.Parts
{
    public class InteractableKeyHole : Interactable
    {
        [SerializeField] private ActionMessageController _message;
        private CarController _car;

        void Start()
        {
            _message = GetMessage();
            _car = transform.parent.GetComponent<CarController>();
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