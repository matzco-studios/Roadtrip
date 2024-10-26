namespace Items
{
    public class CarLight : Mechanics.GrabbableItem
    {
        private float _health;

        public CarLight()
        {
            this._health = 100f;
            Name = "CarLight";
        }
    }
}
