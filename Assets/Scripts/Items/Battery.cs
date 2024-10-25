using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    private Vector3 initialPosition;    // Position d'origine de la batterie
    private Quaternion initialRotation; // Rotation d'origine de la batterie
    private bool isPickedUp = false; // État de prise
    private Transform carHood;

    void Start()
    {
        // Stocke la position et la rotation initiales de la batterie
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
        carHood = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BatteryCollider"))
        {
            transform.SetParent(carHood);
            transform.localPosition = initialPosition;
            transform.localRotation = initialRotation;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void Update()
    {
        
    }
}
