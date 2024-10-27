using UnityEngine;

namespace Items
{
    public class BatteryPickup : MonoBehaviour
    {
        private Vector3 initialPosition; 
        private Quaternion initialRotation; 
        private bool isPickedUp = false; 
        private Transform carHood;
        private Car.CarController carController;


        void Start()
        {
            initialPosition = new Vector3(0.500626624f, 0.195687994f, 1.54742706f);
            initialRotation = transform.localRotation;
            carHood = GameObject.FindGameObjectWithTag("Car").transform.GetChild(0).GetChild(0);
            carController = GameObject.FindGameObjectWithTag("Car").GetComponent<Car.CarController>();

        }   

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("BatteryCollider"))
            {
                transform.SetParent(carHood);
                transform.localPosition = initialPosition;
                transform.localRotation = initialRotation;
                GetComponent<Rigidbody>().isKinematic = true;

                carController.SetBatteryState(false);
                Debug.Log("Batterie insérée dans la voiture");
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("BatteryCollider") && carController != null)
            {
                carController.SetBatteryState(true);
                Debug.Log("Batterie retirée de la voiture");
            }
        }
    }
}