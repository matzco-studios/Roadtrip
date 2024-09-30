using System;
using UnityEngine;

public abstract class IInteractable : MonoBehaviour
{
    public string InteractionInfo;

    public abstract void OnInteract();

    /// <summary>
    /// Function that will display a message to the user how he can interact with the object.
    /// </summary>
    public abstract void InteractionMessage();
}