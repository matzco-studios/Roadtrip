namespace Car.Events.Types
{
    public class BrokenPartEvent : CarEvent
    {
        public override void Activate()
        {
            print("Broken part event.");
        }
    }
}