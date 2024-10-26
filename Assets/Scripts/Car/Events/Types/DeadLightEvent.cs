using Items;
using UnityEngine;

namespace Car.Events.Types
{
    public class DeadLightEvent : CarEvent
    {
        public override void Activate() =>
            Car.carLights[Random.Range(0, 4)].IsWorking = false;
    }
}