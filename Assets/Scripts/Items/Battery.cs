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
        public const int MaxHealth = 100;
        [SerializeField] private float _health = MaxHealth;

        public float Health
        {
            get => _health;
        }

        public bool IsDead() => _health == 0;

        public void SetDead() => _health = 0;

        public void AddHealth(float amount) => _health = Math.Clamp(_health + amount, 0, MaxHealth);

        public void ReduceHealth(float amount) => _health = Math.Clamp(_health - amount, 0, MaxHealth);

        void Start()
        {
            _initialPosition = new Vector3(0.500626624f, 0.15f, 1.54742706f);
            _initialRotation = transform.localRotation;
            _carHood = GameObject.FindGameObjectWithTag("Car").transform.GetChild(0).GetChild(0);
            _carController = GameObject.FindGameObjectWithTag("Car").GetComponent<Car.CarController>();
            _boxColider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BatteryCollider") && _carController.Battery == null)
            {
                transform.SetParent(_carHood);
                transform.localPosition = _initialPosition;
                transform.localRotation = _initialRotation;
                GetComponent<Rigidbody>().isKinematic = true;

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