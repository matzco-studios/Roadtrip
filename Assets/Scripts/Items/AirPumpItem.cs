using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPumpItem : GrabbableItem
{
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

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("WheelOfCar"))
        {
            print(other.gameObject.name);
        }
    }
}
