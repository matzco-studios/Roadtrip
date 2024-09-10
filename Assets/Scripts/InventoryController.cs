using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private GameObject _ePressMessage;

    [SerializeField]
    private GameObject _itemContainer;

    private GameObject _nearItem = null;

    private List<GameObject> _inventoryItems = new();

    private void ShowPressEPanel()
    {
        _ePressMessage.SetActive(true);
    }

    private void HidePressEPanel()
    {
        _ePressMessage.SetActive(true);
    }

    void PutNearItemIntoItemContainer()
    {
        // Need to resee because the container will have multiple component.
        if (_itemContainer.transform.childCount > 0)
        {
            // Set kinematic to false, to let RigidBody properties apply to the object and see it fall.
            //_itemContainer.GetComponentInChildren<Rigidbody>().isKinematic = false;

            //_itemContainer.transform.DetachChildren();
        }

        _nearItem.transform.SetParent(_itemContainer.transform);

        // Set the scale to (1, 1, 1).
        _nearItem.transform.localScale = Vector3.one;

        // To do that the item has the same rotation as the parent.
        _nearItem.transform.localRotation = _itemContainer.transform.localRotation;

        // Set the postion to (0, 0, 0) to let the child(_nearItem) follow the parent Position(_itemContainer).
        _nearItem.transform.localPosition = Vector3.zero;
        _nearItem.GetComponent<Rigidbody>().isKinematic = true;
    }

    void AddInventoryItem()
    {
        if (_inventoryItems.Count == 0)
        {
            PutNearItemIntoItemContainer();
        }
        else if (_inventoryItems.Count == 3)
        {
            _inventoryItems.Remove(_itemContainer);
            PutNearItemIntoItemContainer();
        }

        _inventoryItems.Add(_nearItem);
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
