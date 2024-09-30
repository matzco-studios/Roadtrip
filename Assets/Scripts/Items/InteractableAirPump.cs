using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableAirPump : MonoBehaviour, IInteractable
{
    public string InteractionInfo => throw new System.NotImplementedException();
    private ActionMessageController _message;
    private AirPumpItem _airPumpItem;
    private Animation _airPumpAnim;

    void Start()
    {
        _message = IInteractable.GetActionMessageController();
        _airPumpItem = transform.parent.GetComponent<AirPumpItem>();
        _airPumpAnim = _airPumpItem.GetComponent<Animation>();
    }

    public void InteractionMessage()
    {
        _message.InteractableItem();
    }

    public void OnInteract()
    {
        if (_airPumpItem.IsConnected)
            print("Pump air into tire");
        else 
            print("Pump air for nothing");
        _airPumpAnim.Play();
    }
}
