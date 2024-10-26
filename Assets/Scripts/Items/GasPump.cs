using Car;
using UnityEngine;

namespace Items
{
    public class GasPump : Mechanics.GrabbableItem
    {
        [SerializeField] private UI.ActionMessageController _message;

        private Vector3 fillingPosition;
        private Vector3 initialPosition;
        private Transform fuelTank;
        private GameObject pickedParent;
        private int seconds;


        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("FuelTank"))
            {
                Rotation = Quaternion.Euler(0, 0, 0);
                fillingPosition = new Vector3(1.25f, -0.05f, -0.26f);
                fuelTank = GameObject.FindGameObjectWithTag("FuelTank").transform;
                if (Input.GetKey(KeyCode.E))
                {
                    transform.SetParent(fuelTank);
                    transform.localPosition = fillingPosition;
                    GetComponent<Rigidbody>().isKinematic = true;
                    GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>().Refuel(0.1f);
                }
                else
                {
                    transform.localPosition = initialPosition;
                    transform.SetParent(pickedParent.transform);
                    GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }

        void Update()
        {
            if (gameObject.transform.parent.gameObject.name == "ItemContainer")
            {
                initialPosition = transform.localPosition;
                pickedParent = transform.parent.gameObject;
            }
        }
    }
}