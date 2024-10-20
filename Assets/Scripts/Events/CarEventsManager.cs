using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEventsManager : MonoBehaviour
{
    private List<CarEvent> _events = new();

    private IEnumerator EventLoop()
    {
        while (true)
        {
            var timer = Random.Range(45, 76);
            print($"Next event will be in {timer} seconds.");
            yield return new WaitForSeconds(timer);
        }
    }

    private void Start()
    {
        _events.Add(gameObject.AddComponent<DeadBatteryEvent>());
        _events.Add(gameObject.AddComponent<LightBreakEvent>());
        _events.Add(gameObject.AddComponent<FlatTireEvent>());
        StartCoroutine(EventLoop());
    }
}