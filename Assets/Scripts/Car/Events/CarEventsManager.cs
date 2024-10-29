using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car.Events
{
    public class CarEventsManager : MonoBehaviour
    {
        private readonly List<CarEvent> _events = new();
        private CarController _carController;

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
            _events.Add(gameObject.AddComponent<Types.DeadBatteryEvent>());
            _events.Add(gameObject.AddComponent<Types.LightBreakEvent>());
            _events.Add(gameObject.AddComponent<Types.FlatTireEvent>());
            StartCoroutine(EventLoop());
            
            StartCoroutine(BatteryHealing());
            StartCoroutine(BatteryConsumption());
            StartCoroutine(WheelConsumption());
        }

        /// <summary>
        /// Function that reduces the battery health when the car is not running, the battery is not dead and the lights are on.
        /// This function helps to simulate the case when the user has left the lights on and the car is off.
        /// </summary>
        IEnumerator BatteryConsumption()
        {
            while (true)
            {
                yield return null;

                if (!_carController.IsCarRunning() && _carController.IsLightsOn && _carController.IsBatteryInside() &&
                    !_carController.Battery.IsDead())
                {
                    _carController.Battery.ReduceHealth(Time.deltaTime);
                    print($"Battery consumption: {_carController.Battery.Health}");
                }
            }
        }

        /// <summary>
        /// Function that heals the battery when the car is running, the battery is not dead and not full.
        /// This function helps to simulate the car's alternator.
        /// </summary>
        IEnumerator BatteryHealing()
        {
            while (true)
            {
                yield return null;

                // If the car is running and the battery is not dead, and the battery health is greater than 0 and less than 100.
                if (_carController.IsCarRunning() && _carController.IsBatteryInside() &&
                    !_carController.Battery.IsDead() &&
                    !_carController.Battery.IsFull())
                {
                    _carController.Battery.AddHealth(Time.deltaTime);
                    print($"Battery healing: {_carController.Battery.Health}");
                }
            }
        }

        /// <summary>
        /// This function uses the tires, when the car is moving.
        /// </summary>
        IEnumerator WheelConsumption()
        {
            while (true)
            {
                yield return null;
                
                if (_carController.currentSpeed > 0.10f)
                {
                    print(_carController.currentSpeed);
                    _carController.wheels.ForEach(w =>
                    {
                        var consuming = Time.deltaTime * _carController.currentSpeed / 2.5f;
                        w.ReducePressure((consuming + (Time.deltaTime / 4)) / 50);
                        print($"{w.wheelCollider.name} as {w.Pressure}");
                    });
                }
            }
        }
    }
}