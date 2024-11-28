using Enemies.Scorchlet;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    public class FlashLight : Mechanics.GrabbableItem
    {
        public bool IsTurnedOn = false;
        private Light _light;
        private float _battery = 100f;
        private float _shakeAmnt;
        private Animator _animator;
        private Rigidbody _rigidbody;
        private AudioSource _audioSource;

        public float Battery => _battery;
        
        public void LeftMouse()
        {
            _battery -= 0.8f;
            IsTurnedOn = !IsTurnedOn;
            _light.enabled = true;
            _animator.SetTrigger("Toggle");
        }
        private void Shake()
        {
            _shakeAmnt += 15;
            _battery += 10;
            IsTurnedOn = false;
            _light.enabled = IsTurnedOn;
            if (!_audioSource.isPlaying) {_audioSource.Play();}
        }
        public FlashLight() : base(Quaternion.Euler(-19.109f, -90, -85.682f))
        {
            ActionDictionary.Add(KeyCode.Mouse0, LeftMouse);
            ActionDictionary.Add(KeyCode.Mouse1, Shake);
        }

        void Start()
        {
            _light = transform.GetComponentInChildren<Light>();
            _light.enabled = IsTurnedOn;
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            _battery = Mathf.Clamp(_battery, -2, 150);
            if (IsTurnedOn)
            {
                _light.enabled = !(Mathf.Floor(_battery * _battery * 80) % Mathf.Ceil(_battery * 1.35f) <= Mathf.Sqrt(Mathf.Sqrt(_battery) * 0.865f));
                if (_battery < 0) { LeftMouse(); _battery += 2.6725f; }
                else { _battery -= Time.deltaTime * 22; }
            }
            _animator.enabled = _rigidbody.isKinematic;
            _shakeAmnt += (0-_shakeAmnt)*Time.deltaTime*16;
            if (_shakeAmnt<.1) {_audioSource.Stop();}
            _animator.SetFloat("Shake", _shakeAmnt);
        }

        void FixedUpdate()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.up, out hit, 5))
            {
                if (hit.collider.CompareTag("Scorchlet") && IsTurnedOn)
                {
                    hit.collider.GetComponent<ScorchletController>().IsFlashed();
                }
            }
        }
    }
}