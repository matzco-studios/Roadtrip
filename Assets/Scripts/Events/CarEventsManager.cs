using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEventsManager : MonoBehaviour
{
    private List<CarEvent> _events = new();

    private IEnumerator EventLoop()
    {
        yield return null;
    }
    
    private void Start()
    {
        StartCoroutine(EventLoop());
    }
}
