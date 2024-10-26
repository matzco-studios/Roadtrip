using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class CarLight : Mechanics.GrabbableItem
    {
        private float _health;
        private Light _light;
        private Quaternion _initialRotation;
        
        /**
         * Keys are positions of each lights in fornt of the car
         * -
         * Values true if they're missing
         */
        private KeyValuePair<Vector3, bool> _deadLightsPositions;

        public CarLight()
        {
            this._health = 100f;
            Name = "CarLight";
        }
    }
}
