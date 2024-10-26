using UnityEngine;

namespace Items
{
    public class CarLight : Mechanics.GrabbableItem
    {
        public Light ULight; // U Stand for Unity Light
        public ParticleSystem UFlare;
        
        public bool IsWorking;
        
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;

        private void Start() =>
            IsWorking = true;

        public CarLight()
        {
            Name = "CarLight";
        }
    }
}
