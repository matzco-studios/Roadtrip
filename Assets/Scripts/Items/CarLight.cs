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
            
            GameObject.FindGameObjectWithTag("Car")
                .GetComponent<CarController>().carLights
                .First(l => !l.IsWorking)
                .IsWorking = true;
            
            /*
             * Only destroy the CarLight component not the object
             * -
             * Why : So the player will still have the non-functional light bulb
             */
            Destroy(this); 
        }
    }
}
