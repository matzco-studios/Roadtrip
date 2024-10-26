namespace Items
{
    public class CarLight : GrabbableItem
    {
        private float _health;

        public CarLight()
        {
            this._health = 100f;
            Name = "CarLight";
        }
    }
}