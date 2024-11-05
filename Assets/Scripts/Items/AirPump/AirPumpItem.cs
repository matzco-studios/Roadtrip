using Car.Parts;
using TMPro;
using UnityEngine;

namespace Items.AirPump
{
    public class AirPumpItem : Mechanics.GrabbableItem
    {
        public GameObject IsConnected = null;
        private Rigidbody _rigidbody;
        private LineRenderer _lineRenderer;
        private TextMeshPro _psiDisplay;
        private InteractableAirPump _interactableAirPump;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _lineRenderer = GetComponentInChildren<LineRenderer>();
            _interactableAirPump = GetComponentInChildren<InteractableAirPump>();
            _psiDisplay = GetComponentInChildren<TextMeshPro>();
        }

        void LateUpdate()
        {
            _lineRenderer.SetPosition(0, _lineRenderer.transform.position);
            GameObject oth = IsConnected ? IsConnected : _lineRenderer.gameObject;
            oth = (_lineRenderer.transform.position.y < oth.transform.position.y) ? oth : _lineRenderer.gameObject;
            _lineRenderer.SetPosition(1, oth.transform.position);

            Wheel wheel = _interactableAirPump.GetCurrentWheel();
            _psiDisplay.text = (wheel!=null) ? wheel.Pressure.ToString("00.0"):"--.-";
        }

        void OnTriggerStay(Collider other)
        {
            if (!IsConnected && (!_rigidbody.isKinematic))
            {
                if (other.gameObject.CompareTag("WheelOfCar"))
                {
                    print("Connect to " + other.gameObject.name);
                    IsConnected = other.gameObject;
                }
            }
            else if (_rigidbody.isKinematic)
            {
                IsConnected = null;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("WheelOfCar"))
            {
                IsConnected = null;
                print("Unnconnected");
            }
        }
    }
}