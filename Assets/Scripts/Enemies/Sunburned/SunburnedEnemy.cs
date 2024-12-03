using UnityEngine;
using Animator = UnityEngine.Animator;


namespace Enemies.Sunburned
{
    public class SunburnedEnemy : EnemyController
    {
        private Rigidbody _rigidbody;
        private Renderer  _renderer;
        private Animator  _animator;
        
        private bool _isWatched;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _renderer = GetComponent<Renderer>();
            _animator = transform.GetChild(1).GetComponent<Animator>();
        }

        void Update()
        {
            _isWatched = _renderer.isVisible;
            
            /*
             * Check if player is looking at the sunburned
             * if no, Sunburned is looking at the player & move towards it  
             */
            if (!_isWatched) 
            {
                transform.LookAt(new Vector3(Target.position.x, transform.position.y, Target.position.z));
                transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);

            }
            _rigidbody.isKinematic = _isWatched;
            _animator.enabled = !_isWatched;
        }
    }
}
