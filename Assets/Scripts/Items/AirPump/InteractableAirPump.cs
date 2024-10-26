using UnityEngine;

namespace Items.AirPump
{
    public class InteractableAirPump : Mechanics.Interactable
    {
        [SerializeField] private UI.ActionMessageController _message;
        private AirPumpItem _airPumpItem;
        private Animation _airPumpAnim;

        void Start()
        {
            _airPumpItem = transform.parent.GetComponent<AirPumpItem>();
            _airPumpAnim = _airPumpItem.GetComponent<Animation>();
            _message = GetMessage();
        }

        public override void InteractionMessage()
        {
            _message.InteractableItem("air", "to pump");
        }

        public override void OnInteract()
        {
            if (_airPumpItem.IsConnected)
                print("Pump air into tire");
            else
                print("Pump air for nothing");
            _airPumpAnim.Play();
        }
    }
}