using System.Collections;
using Car;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private const float MaxHealth = 100f;

        [SerializeField] private float _movementSpeed = 3;
        [SerializeField] private float _jumpForce = 5;
        [SerializeField] private Transform _head;
        [SerializeField] private float _coyoteTimeMax = 0.21f;
        [SerializeField] private float _jumpBufferMax = 0.21f;
        [SerializeField] private float _gravityStrength = -9.81f;
        [SerializeField] private float _health = MaxHealth;

        private CharacterController _controller;
        private static CarController _carController;

        private static bool _isGrounded;

        private float _coyoteTime;
        private float _jumpBuffer;
        private Vector3 _inputDir;
        private Vector3 _direction;
        private Vector3 _gravityVelocity = Vector3.zero;
        private static Vector3 _velocity = Vector3.zero;

        private float _healthCooldown = 0;

        public float Health => _health;
        public bool IsDead => _health == 0;

        public void AddHealth(float amount){
            _health = Mathf.Clamp(_health + amount, 0, MaxHealth);
            _healthCooldown = 2f;
        }

        public void ReduceHealth(float amount) {
            _health = Mathf.Clamp(_health - amount, 0, MaxHealth);
            _healthCooldown = 10f;
        }

        public static bool IsWalking =>
            _velocity.magnitude > 0.1 && !_carController.IsPlayerInside && _isGrounded;

        void Start()
        {
            _carController = GameObject.FindGameObjectWithTag("Car")?.GetComponent<CarController>();

            Cursor.lockState = CursorLockMode.Locked;
            _controller = GetComponent<CharacterController>();
        }

        private void HandleJump()
        {
            _coyoteTime -= Time.deltaTime;
            _jumpBuffer -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
                _jumpBuffer = _jumpBufferMax;

            if (_jumpBuffer >= 0 && _coyoteTime >= 0)
                _gravityVelocity.y = _jumpForce;
        }

        private void PlayerDirection()
        {
            _inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            _direction = Quaternion.AngleAxis(_head.eulerAngles.y, Vector3.up) * _inputDir;
        }

        private void Movement()
        {
            var targetVelocity = _movementSpeed * _direction;

            _velocity += (targetVelocity - _velocity) / 15;

            if (_inputDir.Equals(Vector3.zero))
                _velocity += (Vector3.zero - _velocity) / 10;

            _controller.Move(_velocity * Time.fixedDeltaTime);
        }

        private void Gravity()
        {
            _gravityVelocity.y += _gravityStrength * Time.fixedDeltaTime * ((_gravityVelocity.y < 0) ? 1.65f : 1);

            _controller.Move(_gravityVelocity * Time.fixedDeltaTime);

            _isGrounded = _controller.collisionFlags == CollisionFlags.Below;

            if (_isGrounded)
            {
                _gravityVelocity.y = 0;
                _coyoteTime = _coyoteTimeMax;
            }
        }

        IEnumerator DeadCutscene(Rigidbody rb){
            CameraController cameraController = GetComponent<CameraController>();
            rb.AddForce(Random.insideUnitSphere*2f, ForceMode.Impulse);
            while (true){
                yield return null;
                rb.angularDrag+= Time.deltaTime;
                float sens = cameraController.GetSensitivity();
                cameraController.SetSensitivity(sens+((0-sens)*Time.deltaTime*0.3735f));

                if (rb.angularDrag>5.2f){
                    var fade = FindAnyObjectByType<FadeInOut>();
                    fade.timeToFade = 0.35f;
                    fade.FadeIn();
                    yield return new WaitForSeconds(1/fade.timeToFade);
                    SceneManager.LoadScene(0);
                    break;
                }
            }
            
        }

        void Update()
        {
            HandleJump();
            PlayerDirection();

            if (IsDead && enabled){
                enabled = false;
                transform.eulerAngles = Vector3.Scale(transform.eulerAngles, new(1, 1, 0));
                _controller.enabled = false;
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                gameObject.AddComponent<CapsuleCollider>();
                StartCoroutine(DeadCutscene(rb));
            }

            if (_healthCooldown<0){
                AddHealth(5);
            }else{
                _healthCooldown-=Time.deltaTime;
            }
        }

        void FixedUpdate()
        {
            Movement();
            Gravity();
        }

        public void ApplyVelocity(Vector3 force) => _velocity += force;
    }
}