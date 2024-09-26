using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("InteractableItem") && Input.GetKey(KeyCode.F))
        {
            other.GetComponent<IInteractable>().OnInteract();
        }
    }
}
