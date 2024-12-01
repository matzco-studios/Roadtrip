using Items.Mechanics;
using UnityEngine;

namespace Player.Mechanics
{
    /// <summary>
    /// Class used by an ItemContainer to manage child items. 
    /// This class uses the ItemContainer like a backpack.
    /// </summary>
    public class InventoryController : MonoBehaviour
    {
        public static readonly int None = -1, First = 0, Second = 1, Third = 2;
        [SerializeField] private UI.ActionMessageController _message;
        private int _currentSelectedItem = None;
        private float _scrollWheelInput;
        private bool _enabled = true;

        /// <summary>
        /// Function that contain shared logic between DropCurrentItem and AddItem.
        /// </summary>
        /// <param name="item">The item you want to set for grab.</param>
        /// <param name="grab">True mean grab actions, false mean drop actions.</param>
        private void SetItemForGrab(Transform item, bool grab = true)
        {
            item.transform.SetParent(grab ? transform : null);
            var body = item.GetComponent<Rigidbody>();
            body.isKinematic = grab;
            item.GetComponent<Collider>().enabled = !grab;

            if (!grab)
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

            SetItemForGrab(transform.GetChild(_currentSelectedItem).transform, false);
            if (!replace) _currentSelectedItem = None;
        }

        /// <summary>
        /// Function to add an item in the itemsContainer.
        /// </summary>
        /// <param name="nearItem">The item that the is in the Box Collider Trigger of the itemsContainer.</param>
        public void AddItem(GameObject nearItem)
        {
            SetItemForGrab(nearItem.transform);
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
        public void ScrollWheelItemChange()
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

        /// <summary>
        /// Function that lets the user change the _currentSelectedItem, used by several functions like 
        /// KeyboardItemChange and ScrollWheelItemChange.
        /// </summary>
        /// <param name="otherItemIndex">The index of the item that the user wants to select.</param>
        private void SelectAnotherItem(int otherItemIndex)
        {
            if (_currentSelectedItem == otherItemIndex) return;
            if (otherItemIndex > transform.childCount - 1) return;

            SetCurrentItemActive(false);
            ChangeItemVisibility(otherItemIndex, true);
            _currentSelectedItem = otherItemIndex;
        }

        /// <summary>
        /// Function that let the user change the current item using the alpha keys (1, 2 and 3).
        /// </summary>
        public void KeyboardItemChange()
        {
            if (Input.GetKey(KeyCode.Alpha1)) SelectAnotherItem(First);
            else if (Input.GetKey(KeyCode.Alpha2)) SelectAnotherItem(Second);
            else if (Input.GetKey(KeyCode.Alpha3)) SelectAnotherItem(Third);
        }

        /// <summary>
        /// Function that tell if the InventoryController is activated or not.
        /// </summary>
        /// <returns>_enabled value</returns>
        public bool IsActive() => _enabled;

        /// <summary>
        /// Function to activate or desactivate the InventoryController instance.
        /// </summary>
        /// <param name="active">To tell to activate or desactivate.</param>
        public void SetActive(bool active = true)
        {
            SetCurrentItemActive(active);
            _enabled = active;
        }

        public GameObject GetCurrentItem()
        {
            Transform item = transform.GetChild(_currentSelectedItem);
            if (item) return item.gameObject;
            else return null;
        }

        void Update(){
            if (_currentSelectedItem != None)
            {
                Transform item = transform.GetChild(_currentSelectedItem);
                if (item.gameObject.activeInHierarchy)
                {
                    var itemScript = item.GetComponent<GrabbableItem>();
                    if (itemScript)
                    {
                        var keys = itemScript.ActionDictionary.Keys;
                        foreach (var key in keys)
                        {
                            if (Input.GetKeyDown(key))
                            {
                                itemScript.ActionDictionary.TryGetValue(key, out GrabbableItem.KeyAction action);
                                action();
                            }
                        }
                    }
                }
            }
        }
    }
}