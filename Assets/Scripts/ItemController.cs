using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("InteractableItem") && Input.GetKeyDown(KeyCode.E))
        {
            other.GetComponent<IInteractable>().OnInteract();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
