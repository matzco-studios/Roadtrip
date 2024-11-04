using System;
using UnityEngine;

namespace Items
{
    public class Battery : MonoBehaviour
    {
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        private bool _isPickedUp = false;
        private Transform _carHood;
        private Car.CarController _carController;
        private BoxCollider _boxColider;
        private Rigidbody _rigidbody;
        public const float MaxHealth = 100f;
        [SerializeField] private float _health = 80;

        public float Health
        {
            get => _health;
        }

        public bool IsDead() => _health == 0;

        public bool IsFull() => _health == MaxHealth;

        public void SetDead() => _health = 0;

        public void AddHealth(float amount) => _health = Math.Clamp(_health + amount, 0, MaxHealth);

        public void ReduceHealth(float amount) => _health = Math.Clamp(_health - amount, 0, MaxHealth);

        void Start()
        {
            _initialPosition = new Vector3(0.500625f,0.118f,1.547f);
            _initialRotation = transform.localRotation;
            _carHood = GameObject.FindGameObjectWithTag("Car").transform.Find("Body/Body");
            _carController = GameObject.FindGameObjectWithTag("Car").GetComponent<Car.CarController>();
            _boxColider = GetComponent<BoxCollider>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BatteryCollider") && _carController.Battery == null)
            {
                transform.SetParent(_carHood);
                transform.SetLocalPositionAndRotation(_initialPosition, _initialRotation);
                _rigidbody.isKinematic = true;
                _boxColider.enabled = true;
                _carController.Battery = this;
                Debug.Log("Batterie insérée dans la voiture");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("BatteryCollider") && _carController.Battery && _carController.Battery.Equals(this))
            {
                _boxColider.enabled = false;
                _carController.Battery = null;
                Debug.Log("Batterie retirée de la voiture");
            }
        }
    }
}