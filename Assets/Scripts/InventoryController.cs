using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private GameObject _ePressMessage;

    [SerializeField]
    private GameObject _itemContainer;

    private GameObject _nearItem = null;

    [SerializeField]
    private int _currentSelectedItem = 0;

    private void ShowPressEPanel()
    {
        _ePressMessage.SetActive(true);
    }

    private void HidePressEPanel()
    {
        _ePressMessage.SetActive(false);
    }

    void AddInventoryItem()
    {
        if (_itemContainer.transform.childCount == 3)
        {
            var itemToDrop = _itemContainer.transform.GetChild(_currentSelectedItem).transform;
            itemToDrop.GetComponent<Rigidbody>().isKinematic = false;
            itemToDrop.transform.SetParent(null);
        }

        else if (_itemContainer.transform.childCount > 0)
        {
            _nearItem.SetActive(false);
        }

        _nearItem.transform.SetParent(_itemContainer.transform);

        // Set the scale to (1, 1, 1).
        _nearItem.transform.localScale = Vector3.one;

        // To do that the item has the same rotation as the parent.
        _nearItem.transform.localRotation = _itemContainer.transform.localRotation;

        // Set the postion to (0, 0, 0) to let the child(_nearItem) follow the parent Position(_itemContainer).
        _nearItem.transform.localPosition = Vector3.zero;

        // To desactivate RigidBody property.
        _nearItem.GetComponent<Rigidbody>().isKinematic = true;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && _nearItem)
        {
            Debug.Log("Item was get.");

            AddInventoryItem();
            HidePressEPanel();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GrabbableItem"))
        {
            _nearItem = other.gameObject;
            ShowPressEPanel();
        }

        Debug.Log("Entered Trigger");
    }

    void OnTriggerExit()
    {
        _nearItem = null;
        Debug.Log("Exited Trigger");
        HidePressEPanel();
    }
}
