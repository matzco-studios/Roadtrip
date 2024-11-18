using UnityEngine;

namespace Items
{
    public class FlashLight : Mechanics.GrabbableItem
    {
        private bool _isTurnedOn = false;
        private Light _light;
        private void LeftMouse()
        {
            _isTurnedOn = !_isTurnedOn;
            _light.enabled = _isTurnedOn;
        }
        public FlashLight() : base(Quaternion.Euler(-19.109f, -90, -85.682f))
        {
            KeyAction mouseLeft = LeftMouse;
            ActionDictionary.Add(KeyCode.Mouse0, mouseLeft);
        }

        void Start()
        {
            _light = transform.GetComponentInChildren<Light>();
            _light.enabled = _isTurnedOn;
        }
    }
}