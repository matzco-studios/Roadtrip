using System.Collections.Generic;
using System.Linq;
using Car;
using UnityEngine;

namespace Items
{
    public class CarLight : Mechanics.GrabbableItem
    {
        public Light ULight; // U Stand for Unity Light
        public ParticleSystem UFlare;
        
        public bool IsWorking;
        
        private void Start()
        {
            Name = "CarLight";
            IsWorking = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("LightCollider")) return;

            var c = GameObject
                .FindGameObjectWithTag("Car")
                .GetComponent<CarController>().carLights
                .FirstOrDefault(l => !l.IsWorking);
            
            if (!c) return;
            c.ULight.intensity = 1;
            c.UFlare.Play();
            c.IsWorking = true;
            
            Destroy(gameObject); 
        }
    }
}
