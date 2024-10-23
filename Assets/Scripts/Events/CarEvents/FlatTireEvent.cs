using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatTireEvent : CarEvent
{
    public override void Activate()
    {
        print($"The tire number {Random.Range(0, 4)} is dead.");
    }
}
