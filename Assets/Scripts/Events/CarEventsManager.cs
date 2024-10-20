using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEventsManager : MonoBehaviour
{
    private List<CarEvent> _events = new ();
    
    private IEnumerator EventLoop()
    {
        yield return null;
    }
    
    private void Start()
    {
        _events.Add(gameObject.AddComponent<DeadBatteryEvent>());
        _events.Add(gameObject.AddComponent<LightBreakEvent>());
        _events.Add(gameObject.AddComponent<FlatTireEvent>());
        StartCoroutine(EventLoop());
    }
}
