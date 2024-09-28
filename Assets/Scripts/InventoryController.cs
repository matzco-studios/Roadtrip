using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static readonly int None = -1, First = 0, Second = 1, Third = 2;
    [SerializeField] private ActionMessageController _message;
    private int _currentSelectedItem = None;
    private float _scrollWheelInput;
    private bool _enabled = true;

    /// <summary>
    /// Function that contain shared logic between DropCurrentItem and AddItem.
    /// </summary>
    /// <param name="grap">True mean grap actions, false mean drop actions.</param>
    public void SetItemForGrap(Transform item, bool grap = true)
    {
        item.transform.SetParent(grap ? transform : null);
        var body = item.GetComponent<Rigidbody>();
        body.isKinematic = grap;
        item.GetComponent<Collider>().enabled = !grap;

        if (!grap)
        {
            //(move it forward a bit to avoid collisions with player) itemToDrop.transform.Translate(transform.forward*1f);
            body.AddForce(transform.forward * 100 * body.mass);
            body.AddForce(transform.up * 25 * body.mass);
        }
    }

    /// <summary>
    /// Function to drop the current selected item.
    /// </summary>
    /// <param name="replace">True mean he is adding an item and dropping the current one because he exceeded the limit, false mean he is directly dropping the current item.</param>
    public void DropCurrentItem(bool replace = false)
    {
        if (_currentSelectedItem == None) return;

        SetItemForGrap(transform.GetChild(_currentSelectedItem).transform, false);
        if (!replace) _currentSelectedItem = None;
    }

    /// <summary>
    /// Function to add an item in the itemsContainer.
    /// </summary>
    /// <param name="nearItem">The item that the is in the Box Collider Trigger of the itemsContainer.</param>
    public void AddItem(GameObject nearItem)
    {
        SetItemForGrap(nearItem.transform);
        var rotation = nearItem.GetComponent<GrabbableItem>()?.Rotation;
        nearItem.transform.SetLocalPositionAndRotation(Vector3.zero, rotation ?? transform.localRotation);

        if (transform.childCount == 4)
        {
            DropCurrentItem(replace: true);
            nearItem.transform.SetSiblingIndex(_currentSelectedItem);
        }

        else
        {
            SetCurrentItemActive(false);
            _currentSelectedItem = nearItem.transform.GetSiblingIndex();
        }
    }

    /// <summary>
    /// Function to handle scroll wheel input to change the current selected item.
    /// </summary>
    public void ScrollWheelChange()
    {
        if (transform.childCount == 0) return;

        _scrollWheelInput = Input.GetAxisRaw("Mouse ScrollWheel");

        if (_scrollWheelInput > 0)
        {
            SelectAnotherItem(_currentSelectedItem == transform.childCount - 1 ? 0 : _currentSelectedItem + 1);
        }
        else if (_scrollWheelInput < 0)
        {
            SelectAnotherItem(_currentSelectedItem <= 0 ? transform.childCount - 1 : _currentSelectedItem - 1);
        }
    }

    /// <summary>
    /// Function that will get the child with the selectItem index and activate it or desactivate it.
    /// </summary>
    /// <param name="selectItem">The index of the child.</param>
    /// <param name="active">True activate, false desactivate</param>
    private void ChangeItemVisibility(int selectItem, bool active)
    {
        if (selectItem == None) return;
        transform.GetChild(selectItem).gameObject.SetActive(active);
    }

    /// <summary>
    /// Function to activate or desactivate the _currentSelectedItem.
    /// </summary>
    /// <param name="active">True activate, False desactivate.</param>
    private void SetCurrentItemActive(bool active)
    {
        ChangeItemVisibility(_currentSelectedItem, active);
    }

    private void SelectAnotherItem(int otherItemIndex)
    {
        if (_currentSelectedItem == otherItemIndex) return;
        if (otherItemIndex > transform.childCount - 1) return;

        SetCurrentItemActive(false);
        ChangeItemVisibility(otherItemIndex, true);
        _currentSelectedItem = otherItemIndex;
    }

    public void ChangeCurrentItem()
    {
        if (Input.GetKey(KeyCode.Alpha1)) SelectAnotherItem(First);
        else if (Input.GetKey(KeyCode.Alpha2)) SelectAnotherItem(Second);
        else if (Input.GetKey(KeyCode.Alpha3)) SelectAnotherItem(Third);
    }

    public bool IsActive() => _enabled;

    public void SetActive(bool active = true)
    {
        SetCurrentItemActive(active);
        _enabled = active;
    }
}
