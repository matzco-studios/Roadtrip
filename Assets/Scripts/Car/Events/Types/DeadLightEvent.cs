using UnityEngine;

namespace Car.Events.Types
{
    public class DeadLightEvent : CarEvent
    {
        public override void Activate()
        {
            int lightIndex = Random.Range(0, 4);
            Car.carLights[lightIndex].IsWorking = false;
            
            print($"Light number {lightIndex} is broken.");
        }
    }
}