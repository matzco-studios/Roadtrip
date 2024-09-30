using UnityEngine;

public interface IInteractable
{
    string InteractionInfo { get; }

    void OnInteract();

    /// <summary>
    /// Function that will display a message to the user how he can interact with the object.
    /// </summary>
    void InteractionMessage();


    /// <summary>
    /// Function that will display a message to the user how he can interact with the object.
    /// </summary>
    public static ActionMessageController GetActionMessageController() {
        return GameObject.FindWithTag("ActionMessage").GetComponent<ActionMessageController>();
    }
}