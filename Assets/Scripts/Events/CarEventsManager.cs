using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEventsManager : MonoBehaviour
{
    private readonly List<CarEvent> _events = new();
    private CarController _carController;

    private IEnumerator ScheduledLoop()
    {
        while (true)
        {
            // Will be set to 60sec after dev.
            yield return new WaitForSeconds(10);
            print("Scheduled event.");
            _carController.wheels[0].ReducePressure(10);
            print(_carController.wheels[0].Pressure);
        }
    }

    private IEnumerator EventLoop()
    {
        while (true)
        {
            // Range should be between 45 and 76. It is low temporarily for development. 
            var timer = Random.Range(10, 31);
            print($"Event scheduled in {timer} seconds.");
            yield return new WaitForSeconds(timer);

            var index = Random.Range(1, 13) switch
            {
                // LightBreakEvent
                <= 3 => 0,
                // FlatTireEvent
                <= 6 => 1,
                // DeadBatteryEvent
                <= 7 => 2,
                // None
                _ => -1
            };

            if (index != -1)
            {
                _events[index].Activate();
            }
            else
            {
                print("None event.");
            }
        }
    }

    private void Start()
    {
        _carController = GetComponent<CarController>();
        _events.Add(gameObject.AddComponent<DeadBatteryEvent>());
        _events.Add(gameObject.AddComponent<LightBreakEvent>());
        _events.Add(gameObject.AddComponent<FlatTireEvent>());
        StartCoroutine(EventLoop());
        StartCoroutine(ScheduledLoop());
    }
}