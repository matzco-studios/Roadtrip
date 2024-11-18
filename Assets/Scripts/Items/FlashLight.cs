using UnityEngine;

namespace Items
{
    public class FlashLight : Mechanics.GrabbableItem
    {
        private float _battery = 100f;
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
                if (_battery<10){
                    _light.enabled = ! (Mathf.Floor(_battery*300) % Mathf.Ceil(_battery*1.5f)==0);
                }
                if (_battery<=0){ _battery += 2.32f; LeftMouse(); }
                else{ _battery -= Time.deltaTime; }
            }
        }
    }
}