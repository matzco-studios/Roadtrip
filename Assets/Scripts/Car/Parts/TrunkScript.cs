using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car.Parts
{

    public class TrunkScript : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("GrabbableItem") && other.transform.parent == null)
            {
                other.transform.SetParent(transform);
            }
        }
    }
}
