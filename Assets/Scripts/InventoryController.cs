using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject _ePressMessage;
    [SerializeField] private int _currentSelectedItem = 0;

    void AddInventoryItem(GameObject nearItem)
    {
        if (transform.childCount == 3)
        {
            var itemToDrop = transform.GetChild(_currentSelectedItem).transform;
            itemToDrop.GetComponent<Rigidbody>().isKinematic = false;
            nearItem.GetComponent<MeshCollider>().enabled = true;
            itemToDrop.transform.SetParent(null);
        }

        else if (transform.childCount > 0)
        {
            nearItem.SetActive(false);
        }

        nearItem.transform.SetParent(transform);

        // Set the scale to (1, 1, 1).
        nearItem.transform.localScale = Vector3.one;

        // To do that the item has the same rotation as the parent.
        nearItem.transform.localRotation = transform.localRotation;

        // Set the postion to (0, 0, 0) to let the child(nearItem) follow the parent Position(_itemContainer).
        nearItem.transform.localPosition = Vector3.zero;

        // To desactivate RigidBody property.
        nearItem.GetComponent<Rigidbody>().isKinematic = true;
        nearItem.GetComponent<MeshCollider>().enabled = false;

        // Calling OnTriggerExit manually, because it does not activate when we get the item, because we do not leave the trigger zone, 
        // we just desactivate, the item collider and rigidbody.
        OnTriggerExit();
    }

    void FixedUpdate()
    {
        ChangeCurrentItem();
    }

    // The function is executed in loop when two objects are colliding.
    void OnTriggerStay(Collider other)
    {
        print(other.name);

        if (other.CompareTag("GrabbableItem"))
        {
            _ePressMessage.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                AddInventoryItem(other.gameObject);
            }
        }
    }

    void OnTriggerExit()
    {
        print("Exited Trigger");
        _ePressMessage.SetActive(false);
    }

    private void SelectAnotherItem(int otherItemIndex)
    {
        transform.GetChild(_currentSelectedItem).gameObject.SetActive(false);
        transform.GetChild(otherItemIndex).gameObject.SetActive(true);
        _currentSelectedItem = otherItemIndex;
    }

    void ChangeCurrentItem()
    {
        var totalItems = transform.childCount;

        if (Input.GetKey(KeyCode.Keypad1) && _currentSelectedItem != 0 && totalItems >= 1)
        {
            SelectAnotherItem(0);
        }
        else if (Input.GetKey(KeyCode.Keypad2) && _currentSelectedItem != 1 && totalItems >= 2)
        {
            SelectAnotherItem(1);
        }
        else if (Input.GetKey(KeyCode.Keypad3) && _currentSelectedItem != 2 && totalItems >= 3)
        {
            SelectAnotherItem(2);
        }
    }
}
