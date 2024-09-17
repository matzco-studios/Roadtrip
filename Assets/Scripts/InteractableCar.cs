using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCar : MonoBehaviour, IInteractable
{
    private GameObject _player;
    private GameObject _car;
    private Transform _exitOffset;
    private bool _isInCar;
    public string InteractionInfo { get {return "Get in car";} }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _car = transform.parent.gameObject;
        _car.GetComponent<CarController>().IsRunning = false;
        _exitOffset = transform.GetChild(0);
        
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.F)){
            OnInteract();
        }
    }
    private void EnterCar()
    {
        _player.transform.SetParent(GetComponentInParent<Transform>());
        _player.transform.localPosition = Vector3.zero;
        _player.GetComponent<PlayerController>().enabled = false;
        _player.GetComponent<CharacterController>().enabled = false;
        _car.GetComponent<CarController>().IsRunning = true;
        _isInCar = true;
    }
    private void ExitCar()
    {
        if (_car.GetComponent<Rigidbody>().velocity.magnitude<0.1f)
        {
            _player.transform.position = _exitOffset.transform.position;
            _player.transform.SetParent(null);
            Vector3 angle = new(0, _car.transform.eulerAngles.y+10, 0);
            _player.transform.eulerAngles = angle;
            _player.GetComponent<PlayerController>().enabled = true;
            _player.GetComponent<CharacterController>().enabled = true;
            _car.GetComponent<CarController>().IsRunning = false;
            _isInCar = false;
        }
    }
    public void OnInteract()
    {
        if (_isInCar)
            ExitCar();
        else
            EnterCar();
    }
}