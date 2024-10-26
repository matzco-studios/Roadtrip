using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBatteryEvent : CarEvent
{
    public override void Activate()
    {
        print("Battery is dead.");
        Car = GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
    }
}
