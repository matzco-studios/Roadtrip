using Items.Mechanics;

namespace Car.Events.Types
{
    public class DeadBatteryEvent : CarEvent
    {
        public override void Activate()
        {
            if (Car.Battery)
            {
                print("Battery is dead.");
                Car.Battery.SetDead();
                ItemSpawner.SetSpawn();
            }
            else
            {
                print("Car does not have a battery, dead event skipped.");
            }
        }
    }
}