using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private GameObject _ePressMessage;
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private int _currentSelectedItem = 0;

    private void ShowPressEPanel()
    {
        _ePressMessage.SetActive(true);
    }

    private void HidePressEPanel()
    {
        _ePressMessage.SetActive(false);
    }

    void AddInventoryItem(GameObject nearItem)
    {
        if (_itemContainer.transform.childCount == 3)
        {
            var itemToDrop = _itemContainer.transform.GetChild(_currentSelectedItem).transform;
            itemToDrop.GetComponent<Rigidbody>().isKinematic = false;
            nearItem.GetComponent<MeshCollider>().enabled = true;
            itemToDrop.transform.SetParent(null);
        }

        else if (_itemContainer.transform.childCount > 0)
        {
            nearItem.SetActive(false);
        }

        nearItem.transform.SetParent(_itemContainer.transform);

        // Set the scale to (1, 1, 1).
        nearItem.transform.localScale = Vector3.one;

        // To do that the item has the same rotation as the parent.
        nearItem.transform.localRotation = _itemContainer.transform.localRotation;

        // Set the postion to (0, 0, 0) to let the child(nearItem) follow the parent Position(_itemContainer).
        nearItem.transform.localPosition = Vector3.zero;

        // To desactivate RigidBody property.
        nearItem.GetComponent<Rigidbody>().isKinematic = true;
        nearItem.GetComponent<MeshCollider>().enabled = false;

        // Should remove because of OnTriggerExit
        HidePressEPanel();
    }

    void FixedUpdate()
    {
        ChangeCurrentItem();
    }

    // The function is executed in loop when two objects are colliding.
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GrabbableItem"))
        {
            ShowPressEPanel();

            if (Input.GetKeyDown(KeyCode.E))
            {
                AddInventoryItem(other.gameObject);
            }
        }

        print(other.name);
    }

    void OnTriggerExit()
    {
        print("Exited Trigger");
        HidePressEPanel();
    }

    private void SelectAnotherItem(int otherItemIndex)
    {
        _itemContainer.transform.GetChild(_currentSelectedItem).gameObject.SetActive(false);
        _itemContainer.transform.GetChild(otherItemIndex).gameObject.SetActive(true);
        _currentSelectedItem = otherItemIndex;
    }

    void ChangeCurrentItem()
    {
        var totalItems = _itemContainer.transform.childCount;

        if (Input.GetKey(KeyCode.Keypad1) && _currentSelectedItem != 0 && totalItems >= 1)
        {
            SelectAnotherItem(0);
        }
        else if (Input.GetKey(KeyCode.Keypad2) && totalItems >= 2)
        {
            SelectAnotherItem(1);
        }
        else if (Input.GetKey(KeyCode.Keypad3) && totalItems >= 3)
        {
            SelectAnotherItem(2);
        }
    }
}
