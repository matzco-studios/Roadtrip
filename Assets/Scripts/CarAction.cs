using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAction : MonoBehaviour, IInteractable
{
    public GameObject _player;
    private bool _isInCar;
    public string InteractionInfo { get { return "Get in car"; } }
    public void OnInteract()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
