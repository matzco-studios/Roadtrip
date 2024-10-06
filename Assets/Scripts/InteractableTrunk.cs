using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTrunk : Interactable
{
    [SerializeField] private ActionMessageController _message;
    private float _targetRotation = 0f;
    private float _rotation = 0f;
    private float[] _targets = { 0f, 84f };
    private bool _isOpen = false;

    void Start() {
        _message = GetMessage();
    }

    public override void InteractionMessage()
    {
        _message.InteractableItem();
    }

    public override void OnInteract()
    {
        _isOpen = !_isOpen;
        _targetRotation = _targets[_isOpen ? 1 : 0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rotation += (_targetRotation - _rotation) / 30;
        transform.localEulerAngles = new(-_rotation, 0, 0);
    }
}