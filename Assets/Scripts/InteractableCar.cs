using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCar : MonoBehaviour, IInteractable
{
    private GameObject _player;
    private GameObject _car;
    private Transform _exitOffset;
    private Transform _seatPosition;
    private bool _isInCar;
    public string InteractionInfo { get { return "Get in car"; } }

    private ActionMessageController _message;
    [SerializeField] private InventoryController _inventory;

    void Start()
    {
        _message = IInteractable.GetActionMessageController();
        _player = GameObject.FindGameObjectWithTag("Player");
        _car = transform.parent.gameObject;
        _car.GetComponent<CarController>().IsPlayerInside = false;
        _exitOffset = transform.GetChild(0);
        _seatPosition = transform.GetChild(1);
        _seatPosition.SetParent(null);
        _isInCar = false;
    }
    private void EnterCar()
    {
        _isInCar = true;
        _player.transform.SetParent(_seatPosition);
        _player.transform.localPosition = Vector3.zero;
        _player.GetComponent<PlayerController>().enabled = false;
        _player.GetComponent<CharacterController>().enabled = false;
        _car.GetComponent<CarController>().IsPlayerInside = true;
        _inventory.Desactivate();
    }
    private void ExitCar()
    {
        if (_car.GetComponent<Rigidbody>().velocity.magnitude < 0.1f)
        {
            _isInCar = false;
            _player.transform.position = _exitOffset.transform.position;
            _player.transform.SetParent(null);
            Vector3 angle = new(0, _car.transform.eulerAngles.y + 10, 0);
            _player.transform.eulerAngles = angle;
            _player.GetComponent<PlayerController>().enabled = true;
            _player.GetComponent<CharacterController>().enabled = true;
            _car.GetComponent<CarController>().IsPlayerInside = false;
            _inventory.Activate();
        }
    }

    void LateUpdate()
    {
        _seatPosition.position = transform.position;
        _seatPosition.rotation = transform.rotation;
    }

    public void OnInteract()
    {
        if (_isInCar) ExitCar();
        else if (!_isInCar) EnterCar();
    }

    public void InteractionMessage()
    {
        _message.CarInteraction(_isInCar);
    }
}