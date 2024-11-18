using UnityEngine;

namespace Items
{
    public class FlashLight : Mechanics.GrabbableItem
    {
        private float _battery = 0f;
        private bool _isTurnedOn = false;
        private Light _light;
        private void LeftMouse()
        {
            _battery--;
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

        void Update()
        {
            if (_isTurnedOn)
            {
                if (_battery<45){
                    _light.enabled = ! (Mathf.Floor(_battery*_battery*150) % Mathf.Ceil(_battery*3)<=Mathf.Sqrt(Mathf.Sqrt(_battery)*0.865f));
                }
                if (_battery<0){ LeftMouse(); _battery += 2.6725f; }
                else{ _battery -= Time.deltaTime; }
            }
        }
    }
}