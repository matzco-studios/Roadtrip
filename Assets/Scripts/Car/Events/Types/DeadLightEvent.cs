namespace Car.Events.Types
{
    public class DeadLightEvent : CarEvent
    {
        public override void Activate()
        {
            // Kill one of 4 lights
        }
    }
}