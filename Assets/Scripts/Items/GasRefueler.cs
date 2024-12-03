using Car;
using Player.Mechanics;
using UnityEngine;

namespace Items
{
    public class GasRefueler : Mechanics.GrabbableItem
    {
        [SerializeField] private UI.ActionMessageController _message;
        public bool isLimited;
        private bool _isPicked;
        private Vector3 _fillingPosition;
        private Quaternion _fillingRotation;
        private Vector3 _pickedPosition;
        private Quaternion _pickedRotation;
        private Transform fuelTank;
        private GameObject _pickedParent;
        private Rigidbody rb;
        [SerializeField, Range(0.1f, 1)] private float _fuelAmount;
        private BoxCollider triggerBox;
        private GameObject _initialParent;
        private Vector3 _initialPosition;
        private Vector3 _initialRotation;
        private AudioSource _refillSound;

        void Start()
        {
            if (!isLimited) _fuelAmount = float.PositiveInfinity;
            _refillSound = GetComponent<AudioSource>();
            if (!isLimited) _initialParent = transform.parent.gameObject;
            _initialPosition = transform.position;
            _initialRotation = transform.rotation.eulerAngles;
            _isPicked = false;
            _pickedPosition = new Vector3(0, 0, 0);
            _pickedParent = null;
            rb = GetComponent<Rigidbody>();
            triggerBox = GetComponent<BoxCollider>();
        }

        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("FuelTank"))
            {
                if (isLimited)
                {
                    _fillingPosition = new Vector3(1.25f, 0.5f, -0.26f);
                    _fillingRotation = Quaternion.Euler(0, 0, 118);
                }
                else
                {
                    _fillingPosition = new Vector3(1.25f, -0.15f, -0.26f);
                    _fillingRotation = Quaternion.Euler(0, -90, 0);
                }
                fuelTank = GameObject.FindGameObjectWithTag("FuelTank").transform;
                if (Input.GetKey(KeyCode.E))
                {
                    if (_fuelAmount > 0)
                    {
                        if (!_refillSound.isPlaying) _refillSound.Play();
                        triggerBox.excludeLayers = LayerMask.GetMask("Ignore Raycast");
                        transform.SetParent(fuelTank);
                        transform.SetLocalPositionAndRotation(_fillingPosition, _fillingRotation);
                        GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>().Refuel(0.1f);
                        _fuelAmount -= 0.1f * Time.fixedDeltaTime;
                        if (GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>().currentFuel >= 100)
                        {
                            _refillSound.Stop();
                        }
                    }
                    else
                    {
                        _refillSound.Stop();
                        if (isLimited) ResetPosition(true);
                    }
                }
                else if (rb.isKinematic)
                {
                    _refillSound.Stop();
                    ResetPosition(false);
                }
            }
            else if (other.gameObject.CompareTag("GasMachine") && !_isPicked && (transform.parent == null || transform.parent == _initialParent.transform))
            {
                transform.SetPositionAndRotation(_initialPosition, Quaternion.Euler(_initialRotation));
                transform.SetParent(_initialParent.transform);
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        void ResetPosition(bool _isEmpty)
        {
            if (_isEmpty) GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InventoryController>().DropCurrentItem();
            else
            {
                triggerBox.excludeLayers = LayerMask.GetMask();
                transform.SetLocalPositionAndRotation(_pickedPosition, _pickedRotation);
                transform.SetParent(_pickedParent.transform);
            }
        }

        void FixedUpdate()
        {
            if (transform.parent != null && transform.parent.gameObject != null)
            {
                if (gameObject.transform.parent.gameObject.name == "ItemContainer")
                {
                    _isPicked = true;
                    rb.useGravity = false;
                    _pickedPosition = transform.localPosition;
                    _pickedRotation = transform.localRotation;
                    _pickedParent = transform.parent.gameObject;
                }
                else
                {
                    _isPicked = false;
                    rb.constraints = RigidbodyConstraints.None;
                }
            }
            else if (transform.parent == null)
            {
                _isPicked = false;
                rb.constraints = RigidbodyConstraints.None;
                rb.useGravity = true;
            }
        }
    }
}