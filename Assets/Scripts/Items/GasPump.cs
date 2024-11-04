using Car;
using Player.Mechanics;
using UnityEngine;

namespace Items
{
    public class GasPump : Mechanics.GrabbableItem
    {
        [SerializeField] private UI.ActionMessageController _message;
        private bool _isPicked;
        private Vector3 _fillingPosition;
        private Quaternion _fillingRotation;
        private Vector3 _pickedPosition;
        private Quaternion _pickedRotation;
        private Transform fuelTank;
        private GameObject _pickedParent;
        private GameObject _initialParent;
        private Vector3 _initialPosition;
        private Vector3 _initialRotation;
        private Rigidbody rb;

        void Start()
        {
            _initialPosition = transform.position;
            _initialRotation = transform.rotation.eulerAngles;
            _isPicked = false;
            _pickedPosition = new Vector3(0, 0, 0);
            _pickedParent = null;
            _initialParent = transform.parent.gameObject;
            rb = GetComponent<Rigidbody>();
        }

        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("FuelTank"))
            {
                _fillingPosition = new Vector3(1.25f, -0.15f, -0.26f);
                _fillingRotation = Quaternion.Euler(0, -90, 0);
                fuelTank = GameObject.FindGameObjectWithTag("FuelTank").transform;
                if (Input.GetKey(KeyCode.E))
                {
                    transform.SetParent(fuelTank);
                    transform.localPosition = _fillingPosition;
                    transform.localRotation = _fillingRotation;
                    GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>().Refuel(0.1f);
                }
                else if (rb.isKinematic)
                {
                    transform.localPosition = _pickedPosition;
                    transform.localRotation = _pickedRotation;
                    transform.SetParent(_pickedParent.transform);
                }
            }
            else if (other.gameObject.CompareTag("GasMachine") && !_isPicked)
            {
                transform.position = _initialPosition;
                transform.rotation = Quaternion.Euler(_initialRotation);
                transform.SetParent(_initialParent.transform);
                rb.constraints = RigidbodyConstraints.FreezeAll;
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