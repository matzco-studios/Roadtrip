using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car.Parts
{

    public class TrunkScript : MonoBehaviour
    {
        private Rigidbody rb;
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("GrabbableItem") && other.transform.parent == null)
            {
                other.transform.SetParent(transform);
                other.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
