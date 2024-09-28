using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private ActionMessageController _message;
    [SerializeField] private InventoryController _inventory;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("GrabbableItem"))
        {
            _message.GrabItem(collider.gameObject);
        }
        else if (collider.CompareTag("InteractableItem"))
        {
            collider.gameObject.GetComponent<IInteractable>().InteractionMessage();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("InteractableItem") && Input.GetKey(KeyCode.F))
        {
            other.GetComponent<IInteractable>().OnInteract();
        }
        else if (other.CompareTag("GrabbableItem"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                _inventory.AddItem(other.gameObject);

                // Calling OnTriggerExit manually, because it does not activate when we get the item, because we do not leave the trigger zone, 
                // we just desactivate, the item collider and rigidbody.
                OnTriggerExit();
            }
        }
    }

    void OnTriggerExit()
    {
        _message.Disable();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _inventory.DropCurrentItem();
        }

        _inventory.ScrollWheelChange();
        _inventory.ChangeCurrentItem();
    }
}
