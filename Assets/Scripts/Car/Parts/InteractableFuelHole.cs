using Items;
using Player.Mechanics;
using UnityEngine;

namespace Car.Parts
{
    public class InteractableFuelHole : Items.Mechanics.Interactable
    {
        [SerializeField] private UI.ActionMessageController _message;
        private InventoryController _inventoryController;

        void Start()
        {
            _message = GetMessage();
            _inventoryController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InventoryController>();
        }

        public override void InteractionMessage()
        {
            GameObject item = _inventoryController.GetCurrentItem();
            if (item && item.GetComponent<GasRefueler>())
                _message.InteractableItem("in Car.","to put Fuel","E");
        }

        public override void OnInteract()
        {
            
        }
    }
}