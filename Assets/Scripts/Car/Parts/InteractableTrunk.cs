using System.Net;
using UnityEngine;

namespace Car.Parts
{
    public class InteractableTrunk : Items.Mechanics.Interactable
    {
        [SerializeField] private UI.ActionMessageController _message;
        private float _targetRotation = 0f;
        private float _rotation = 0f;
        private bool _open = false;
        private bool _wasInside = false;
        private CarController _car;
        private Collider _box;
        public float[] Targets = { 0f, 84f };
        public float Speed = 20f;

        void Start()
        {
            _message = GetMessage();
            _car = GetCar();
            _box = GetComponent<Collider>();
        }

        public override void InteractionMessage()
        {
            _message.InteractableItem(InteractionInfo, _open ? "to close" : "to open");
        }

        public override void OnInteract()
        {
            _open = !_open;
            _targetRotation = Targets[_open ? 1 : 0];
        }

        public void CheckCarEnter()
        {
            if (_car.IsPlayerInside != _wasInside)
            {
                //_open = false; 
                //_targetRotation = Targets[0];
                _box.enabled = !_car.IsPlayerInside;
            }

            _wasInside = _car.IsPlayerInside;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            _rotation += (_targetRotation - _rotation) / Speed;
            transform.localEulerAngles = new(-_rotation, 0, 0);

            CheckCarEnter();
        }

        void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player") && Mathf.Abs(_targetRotation - _rotation)>12)
            {
                print("Entered");
                OnInteract();
            }
        }
    }
}