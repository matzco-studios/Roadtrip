using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCar : MonoBehaviour, IInteractable
{
    private GameObject _player;
    private GameObject _car;
    public string InteractionInfo { get {return "Get in car";} }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _car = transform.parent.gameObject;
        _car.GetComponent<CarController>().IsRunning = false;
        
    }
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnInteract();
        }
    }
    public void OnInteract()
    {
        _player.transform.SetParent(GetComponentInParent<Transform>());
        _player.transform.localPosition = Vector3.zero;
        _player.GetComponent<PlayerController>().enabled = false;
        _player.GetComponent<CharacterController>().enabled = false;
        _car.GetComponent<CarController>().IsRunning = true;
    }
}