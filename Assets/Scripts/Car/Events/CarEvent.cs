using Car;
using UnityEngine;

namespace Car.Events
{
    public abstract class CarEvent : MonoBehaviour
    {
        protected CarController Car;

        private void Start()
        {
            Car = GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
        }

        public abstract void Activate();
    }   
}
