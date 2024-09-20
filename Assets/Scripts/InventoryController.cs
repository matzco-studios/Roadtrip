using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public enum SelectItem
    {
        None = -1, First, Second, Third
    }

    [SerializeField] private ActionMessageController _message;
    private SelectItem _currentSelectedItem = SelectItem.None;
    private float _scrollWheelInput;

    /// <summary>
    /// Function to drop the current selected item.
    /// </summary>
    /// <param name="replace">True mean he is adding an item and dropping the current one because he exceeded the limit, false mean he is directly dropping the current item.</param>
    void DropCurrentItem(bool replace = false)
    {
        if (_currentSelectedItem == SelectItem.None) return;

        var itemToDrop = transform.GetChild((int)_currentSelectedItem).transform;
        itemToDrop.GetComponent<Rigidbody>().isKinematic = false;
        itemToDrop.GetComponent<MeshCollider>().enabled = true;
        itemToDrop.transform.SetParent(null);
        print(itemToDrop.name);

        if (!replace) _currentSelectedItem = SelectItem.None;
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
            nearItem.transform.SetSiblingIndex((int)_currentSelectedItem);
        }

        else if(_currentSelectedItem == SelectItem.None) {
            _currentSelectedItem = (SelectItem) nearItem.transform.GetSiblingIndex();
        }

        else if (transform.childCount > 1)
        {
            nearItem.SetActive(false);
        }
        else
        {
            _currentSelectedItem = SelectItem.First;
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
        if (transform.childCount == 0) return;

        _scrollWheelInput = Input.GetAxisRaw("Mouse ScrollWheel");

        if (_scrollWheelInput > 0)
        {
            SelectAnotherItem((int)_currentSelectedItem == transform.childCount - 1 ? 0 : _currentSelectedItem + 1);
        }
        else if (_scrollWheelInput < 0)
        {
            SelectAnotherItem(_currentSelectedItem <= 0 ? (SelectItem) transform.childCount - 1 : _currentSelectedItem - 1);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropCurrentItem();
        }

        ScrollWheelChange();
        ChangeCurrentItem();
    }

    // The function is executed in loop when two objects are colliding.
    void OnTriggerStay(Collider other)
    {
        print(other.name);

        if (other.CompareTag("GrabbableItem"))
        {
            _message.GrapItem(other.name);

            if (Input.GetKey(KeyCode.E))
            {
                AddItem(other.gameObject);
            }
        }
    }

    void OnTriggerExit()
    {
        print("Exited Trigger");
        _message.Disable();
    }

    private void SelectAnotherItem(SelectItem otherItemIndex)
    {
        if (_currentSelectedItem != SelectItem.None)
        {
            transform.GetChild((int)_currentSelectedItem).gameObject.SetActive(false);
        }

        transform.GetChild((int)otherItemIndex).gameObject.SetActive(true);
        _currentSelectedItem = otherItemIndex;
    }

    void ChangeCurrentItem()
    {
        var totalItems = transform.childCount;

        if (Input.GetKey(KeyCode.Alpha1) && _currentSelectedItem != SelectItem.First && totalItems >= 1)
        {
            SelectAnotherItem(SelectItem.First);
        }
        else if (Input.GetKey(KeyCode.Alpha2) && _currentSelectedItem != SelectItem.Second && totalItems >= 2)
        {
            SelectAnotherItem(SelectItem.Second);
        }
        else if (Input.GetKey(KeyCode.Alpha3) && _currentSelectedItem != SelectItem.Third && totalItems >= 3)
        {
            SelectAnotherItem(SelectItem.Third);
        }
    }
}
