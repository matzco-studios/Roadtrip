using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject _ePressMessage;
    [SerializeField] private int _currentSelectedItem = 0;

    void DropCurrentItem(bool replace = false)
    {
        var itemToDrop = transform.GetChild(_currentSelectedItem).transform;
        itemToDrop.GetComponent<Rigidbody>().isKinematic = false;
        itemToDrop.GetComponent<MeshCollider>().enabled = true;
        itemToDrop.transform.SetParent(null);
        print(itemToDrop.name);

        if (!replace)
        {
            // code the execute if we are just dropping the item without replacing it.
            print("Not replace");
        }
    }

    void AddItem(GameObject nearItem)
    {
        nearItem.transform.SetParent(transform);
        nearItem.GetComponent<Rigidbody>().isKinematic = true;
        nearItem.GetComponent<Collider>().enabled = false;
        nearItem.transform.localScale = Vector3.one;

        // To do give a position of (0, 0, 0) to let the child follow the parent position and applied the rotation of the parent.
        nearItem.transform.SetLocalPositionAndRotation(Vector3.zero, transform.localRotation);

        if (transform.childCount == 4)
        {
            DropCurrentItem(replace: true);
            nearItem.transform.SetSiblingIndex(_currentSelectedItem);
        }

        else if (transform.childCount > 1)
        {
            nearItem.SetActive(false);
        }

        // Calling OnTriggerExit manually, because it does not activate when we get the item, because we do not leave the trigger zone, 
        // we just desactivate, the item collider and rigidbody.
        OnTriggerExit();
    }

    void FixedUpdate()
    {
        ChangeCurrentItem();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropCurrentItem();
        }
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
                AddItem(other.gameObject);
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
