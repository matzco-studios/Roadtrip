using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPumpItem : GrabbableItem
{
    protected GameObject IsConnected = null;
    private Rigidbody _rigidbody;
    private LineRenderer _lineRenderer;
    public override void OnCustomAction()
    {
        throw new System.NotImplementedException();
    }

    public override void OnLeftClick()
    {
        throw new System.NotImplementedException();
    }

    public override void OnRightClick()
    {
        throw new System.NotImplementedException();
    }

    void Start() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        _lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    void LateUpdate()
    {
        _lineRenderer.SetPosition(0, _lineRenderer.transform.position);
        GameObject oth = IsConnected ? IsConnected : _lineRenderer.gameObject;
        _lineRenderer.SetPosition(1, oth.transform.position);
    }

    void OnTriggerStay(Collider other) 
    {
        if (!IsConnected && (!_rigidbody.isKinematic))
        {
            if (other.gameObject.CompareTag("WheelOfCar"))
            {
                print("Connect to " + other.gameObject.name);
                IsConnected = other.gameObject;
            }
        }else if (_rigidbody.isKinematic)
        {
            IsConnected = null;
        }
    }
    void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.CompareTag("WheelOfCar"))
        {
            IsConnected = null;
            print("Unnconnected");
        }
    }
}
