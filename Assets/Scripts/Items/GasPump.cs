using UnityEngine;

namespace Items
{
    public class GasPump : Mechanics.GrabbableItem
    {
        [SerializeField] private UI.ActionMessageController _message;

        private Vector3 fillingPosition;
        private Vector3 initialPosition;
        private Transform fuelTank;
        private GameObject initialParent;

        void Start()
        {
            Rotation = Quaternion.Euler(0, 0, 0);
            initialParent = transform.parent.gameObject;
            initialPosition = transform.localPosition;
            fillingPosition = new Vector3(1.25f, -0.05f, -0.26f);
            fuelTank = GameObject.FindGameObjectWithTag("FuelTank").transform;
        }

        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("FuelTank"))
            {
                if (Input.GetKey(KeyCode.E))
                {
                    transform.SetParent(fuelTank);
                    transform.localPosition = fillingPosition;
                    GetComponent<Rigidbody>().isKinematic = true;
                }
                else
                {
                    transform.SetParent(initialParent.transform);
                    transform.localPosition = initialPosition;
                    GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }
    }
}