using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject _ePressMessage;
    [SerializeField] private int _currentSelectedItem = -1;

    /// <summary>
    /// Function to drop the current selected item.
    /// </summary>
    /// <param name="replace">True mean he is adding an item and dropping the current one because he exceeded the limit, false mean he is directly dropping the current item.</param>
    void DropCurrentItem(bool replace = false)
    {
        if (_currentSelectedItem == -1) return;

        var itemToDrop = transform.GetChild(_currentSelectedItem).transform;
        itemToDrop.GetComponent<Rigidbody>().isKinematic = false;
        itemToDrop.GetComponent<MeshCollider>().enabled = true;
        itemToDrop.transform.SetParent(null);
        print(itemToDrop.name);

        if (!replace)
        {
            if (transform.childCount == 0)
            {
                _currentSelectedItem = -1;
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
                _currentSelectedItem = 0;
            }
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
        else
        {
            _currentSelectedItem = 0;
        }

        // Calling OnTriggerExit manually, because it does not activate when we get the item, because we do not leave the trigger zone, 
        // we just desactivate, the item collider and rigidbody.
        OnTriggerExit();
    }

    /// <summary>
    /// Function to handle scroll wheel input to change the current selected item.
    /// </summary>
    void ScrollWheelChange()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            SelectAnotherItem(_currentSelectedItem < 2 ? _currentSelectedItem + 1 : 0);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            SelectAnotherItem(_currentSelectedItem > 0 ? _currentSelectedItem - 1 : 2);
        }
    }

    void FixedUpdate()
    {
        ScrollWheelChange();
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

        if (Input.GetKey(KeyCode.Alpha1) && _currentSelectedItem != 0 && totalItems >= 1)
        {
            SelectAnotherItem(0);
        }
        else if (Input.GetKey(KeyCode.Alpha2) && _currentSelectedItem != 1 && totalItems >= 2)
        {
            SelectAnotherItem(1);
        }
        else if (Input.GetKey(KeyCode.Alpha3) && _currentSelectedItem != 2 && totalItems >= 3)
        {
            SelectAnotherItem(2);
        }
    }
}
