using System.Linq;
using Car.Parts;
using UnityEngine;

namespace Car.Events.Types
{
    public class FlatTireEvent : CarEvent
    {
        public override void Activate()
        {
            int tire = Random.Range(0, 4);
            Wheel wheel = Car.wheels.ElementAt(tire);
            wheel.FlatTire();
            print($"The tire number {tire} is dead.");
        }
    }
}