using UnityEngine;

namespace Car.Events.Types
{
    public class LightBreakEvent : CarEvent
    {
        public override void Activate()
        {
            print($"Light number {Random.Range(0, 2)} is broken.");
        }
    }
}