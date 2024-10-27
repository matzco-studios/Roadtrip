using System.Collections;
using System.Collections.Generic;
using Car.Events.Types;
using UnityEngine;

namespace Car.Events
{
    public class CarEventsManager : MonoBehaviour
    {
        private readonly List<CarEvent> _events = new();

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

                if (true)
                {
                    _events[0].Activate();
                }
                else
                {
                    print("None event.");
                }
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
}