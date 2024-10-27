using UnityEngine;

namespace Items
{
    public class BatteryPickup : MonoBehaviour
    {
        private Vector3 _initialPosition; 
        private Quaternion _initialRotation; 
        private bool _isPickedUp = false; 
        private Transform _carHood;
        private Car.CarController _carController;
        private BoxCollider _boxColider;

        void Start()
        {
            _initialPosition = new Vector3(0.500626624f, 0.195687994f, 1.54742706f);
            _initialRotation = transform.localRotation;
            _carHood = GameObject.FindGameObjectWithTag("Car").transform.GetChild(0).GetChild(0);
            _carController = GameObject.FindGameObjectWithTag("Car").GetComponent<Car.CarController>();
            _boxColider = GetComponent<BoxCollider>();


        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BatteryCollider"))
            {
                transform.SetParent(_carHood);
                transform.localPosition = _initialPosition;
                transform.localRotation = _initialRotation;
                GetComponent<Rigidbody>().isKinematic = true;

                _boxColider.enabled = true;
                _carController.SetBatteryState(false);
                Debug.Log("Batterie insérée dans la voiture");
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("BatteryCollider") && _carController != null)
            {
                _boxColider.enabled = false;
                _carController.SetBatteryState(true);
                Debug.Log("Batterie retirée de la voiture");
                

            }
        }
    }
}