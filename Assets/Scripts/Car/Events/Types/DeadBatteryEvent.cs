namespace Car.Events.Types
{
    public class DeadBatteryEvent : CarEvent
    {
        public override void Activate()
        {
            //print("Battery is dead.");
        }
    }
}