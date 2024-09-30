using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private ActionMessageController _message;
    [SerializeField] private InventoryController _inventory;
    private int _keyPressE;
    private int _keyPressF;

    private void ForceShowMessage(Collider other)
    {
        if (!_message.gameObject.activeSelf)
            OnTriggerEnter(other);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (_inventory.IsActive() && collider.CompareTag("GrabbableItem"))
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
        if (other.CompareTag("InteractableItem"))
        {
            if (_keyPressF>0){
                other.GetComponent<IInteractable>().OnInteract();
                _keyPressF = 0;
            }else
                ForceShowMessage(other);
        }
        else if (_inventory.IsActive() && other.CompareTag("GrabbableItem"))
        {
            if (_keyPressE>0)
            {
                _inventory.AddItem(other.gameObject);
                _keyPressE = 0;

                // Calling OnTriggerExit manually, because it does not activate when we get the item, because we do not leave the trigger zone, 
                // we just desactivate, the item collider and rigidbody.
                OnTriggerExit(other);
            }else{
                ForceShowMessage(other);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractableItem") || other.CompareTag("GrabbableItem"))
        {
            _message.Disable();
        }
    }

    void Update()
    {
        if (_inventory.IsActive())
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _inventory.DropCurrentItem();
            }

            _inventory.ScrollWheelItemChange();
            _inventory.KeyboardItemChange();
        }
        if (Input.GetKeyDown(KeyCode.E))
            _keyPressE = 2;
        if (Input.GetKeyDown(KeyCode.F))
            _keyPressF = 2;
    }
    void FixedUpdate()
    {
        _keyPressE--;
        _keyPressF--;
    }
}
