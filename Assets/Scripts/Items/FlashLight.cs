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
            _battery -= 0.8f;
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
                _light.enabled = !(Mathf.Floor(_battery * _battery * 185) % Mathf.Ceil(_battery * 3.35f) <= Mathf.Sqrt(Mathf.Sqrt(_battery) * 0.865f));
                if (_battery < 0) { LeftMouse(); _battery += 2.6725f; }
                else { _battery -= Time.deltaTime; }
            }
        }

        void FixedUpdate()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.up, out hit, 5))
            {
                if (hit.collider.CompareTag("Scorchlet") && _isTurnedOn)
                {
                    hit.collider.GetComponent<ScorchletController>().IsFlashed();
                }
            }
        }
    }
}