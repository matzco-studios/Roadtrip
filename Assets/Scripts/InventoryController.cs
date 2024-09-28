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
        var body = itemToDrop.GetComponent<Rigidbody>();
        body.isKinematic = false;
        itemToDrop.GetComponent<Collider>().enabled = true;
        itemToDrop.transform.SetParent(null);
        //(move it forward a bit to avoid collisions with player) itemToDrop.transform.Translate(transform.forward*1f);
        body.AddForce(transform.forward*100 * body.mass);
        body.AddForce(transform.up*25 * body.mass);
        //print(itemToDrop.name);

        if (!replace) _currentSelectedItem = SelectItem.None;
    }

    /// <summary>
    /// Function to add an item in the itemsContainer.
    /// </summary>
    /// <param name="nearItem">The item that the is in the Box Collider Trigger of the itemsContainer.</param>
    void AddItem(GameObject nearItem)
    {
        nearItem.transform.SetParent(transform);
        nearItem.GetComponent<Rigidbody>().isKinematic = true;
        nearItem.GetComponent<Collider>().enabled = false;

        var nearItemScript = nearItem.GetComponent<GrabbableItem>();

        nearItem.transform.SetLocalPositionAndRotation(
            Vector3.zero,
            nearItemScript ? nearItemScript.Rotation : transform.localRotation
        );

        if (transform.childCount == 4)
        {
            DropCurrentItem(replace: true);
            nearItem.transform.SetSiblingIndex((int)_currentSelectedItem);
        }

        else
        {
            if (_currentSelectedItem != SelectItem.None)
            {
                transform.GetChild((int)_currentSelectedItem).gameObject.SetActive(false);
            }

            _currentSelectedItem = (SelectItem)nearItem.transform.GetSiblingIndex();
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
            SelectAnotherItem(_currentSelectedItem <= 0 ? (SelectItem)transform.childCount - 1 : _currentSelectedItem - 1);
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
        //print(other.name);

        if (other.CompareTag("GrabbableItem"))
        {
            _message.GrabItem(other.name);

            if (Input.GetKey(KeyCode.E))
            {
                AddItem(other.gameObject);
            }
        }
    }

    void OnTriggerExit()
    {
        if (_message) _message.Disable();
        //print("Exited Trigger");
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
