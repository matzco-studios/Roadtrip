using Car;
using UnityEngine;

namespace Items.Mechanics
{
    public abstract class Interactable : MonoBehaviour
    {
        protected string InteractionInfo { get; set; }

        public abstract void OnInteract();

        /// <summary>
        /// Function that will display a message to the user how he can interact with the object.
        /// </summary>
        public abstract void InteractionMessage();

        protected UI.ActionMessageController GetMessage()
        {
            return GameObject.FindGameObjectWithTag("ActionMessage").GetComponent<UI.ActionMessageController>();
        }

        protected CarController GetCar()
        {
            return GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
        }
    }
}