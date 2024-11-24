using System;
using UnityEngine;

namespace Car.Parts
{
    [Serializable]
    public class Wheel
    {
        public const float MaxPsi = 38;
        public GameObject wheelObject;
        public WheelCollider wheelCollider;
        public bool isFrontWheel;
        public bool isFlat;
        
        private float _pressure = 33;

        public float Pressure
        {
            get => _pressure;
            set
            {
                _pressure = value;
                isFlat = value > 0;
            }
        }

        public void FlatTire()
        {
            _pressure = 0;
        } 
        public void AddPressure(float amount) => _pressure = Math.Clamp(_pressure + amount, 0, MaxPsi);
        public void ReducePressure(float amount) => _pressure = Math.Clamp(_pressure - amount, 0, MaxPsi);
    }
}