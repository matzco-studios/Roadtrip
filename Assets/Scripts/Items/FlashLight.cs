using UnityEngine;

namespace Items
{
    public class FlashLight : Mechanics.GrabbableItem
    {
        private float _battery = 100f;
        private bool _isTurnedOn = false;
        private Light _light;
        private float _shakeAmnt;
        private Animator _animator;
        private Rigidbody _rigidbody;
        private AudioSource _audioSource;
        private void LeftMouse()
        {
            _battery-=0.8f;
            _isTurnedOn = !_isTurnedOn;
            _light.enabled = _isTurnedOn;
            _animator.SetTrigger("Toggle");
        }
        private void Shake()
        {
            _shakeAmnt += 15;
            _battery += 10;
            _isTurnedOn = false;
            _light.enabled = _isTurnedOn;
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
            _light.enabled = _isTurnedOn;
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            _battery = Mathf.Clamp(_battery, -2, 150);
            if (_isTurnedOn)
            {
                _light.enabled = ! (Mathf.Floor(_battery*_battery*80) % Mathf.Ceil(_battery*1.35f)<=Mathf.Sqrt(Mathf.Sqrt(_battery)*0.865f));
                if (_battery<0){ LeftMouse(); _battery += 2.6725f; }
                else{ _battery -= Time.deltaTime*22; }
            }
            _animator.enabled = _rigidbody.isKinematic;
            _shakeAmnt += (0-_shakeAmnt)*Time.deltaTime*16;
            if (_shakeAmnt<.1) {_audioSource.Stop();}
            _animator.SetFloat("Shake", _shakeAmnt);
        }
    }
}