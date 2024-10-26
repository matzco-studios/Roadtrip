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
        initialPosition = new Vector3(0.500626624f, 0.195687994f, 1.54742706f);
        initialRotation = transform.localRotation;
        carHood = GameObject.FindGameObjectWithTag("Car").transform.GetChild(0).GetChild(0);
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
}
