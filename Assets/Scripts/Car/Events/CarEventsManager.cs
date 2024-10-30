using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car.Events
{
    public class CarEventsManager : MonoBehaviour
    {
        private CarController _carController;
        private readonly List<CarEvent> _carEvents = new();
        private readonly Dictionary<CarEventType, int> _probabilities = new()
        {
            { CarEventType.DeadBatteryEvent, 1 },
            { CarEventType.LightBreakEvent, 3 },
            { CarEventType.FlatTireEvent, 3 },
            { CarEventType.BrokenPartEvent, 2 },
            { CarEventType.None, 5 },
        };
        
        // A list that will be generated using the _probabilities field.
        private readonly List<CarEventType> _eventTypesList = new();
        
        /// <summary>
        /// Function that randomly activates events that affect the car in a bad way
        /// to simulate the player experience.
        /// </summary>
        private IEnumerator EventLoop()
        {
            while (true)
            {
                // Range should be between 45 and 76. It is low temporarily for development. 
                var timer = Random.Range(10, 31);
                print($"Event scheduled in {timer} seconds.");
                yield return new WaitForSeconds(timer);

                var eventType = _eventTypesList[Random.Range(0, _eventTypesList.Count)];
                if (eventType != CarEventType.None)
                {
                    _carEvents[(int)eventType].Activate();
                }
                
                else print("No event triggered.");
            }
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

        /// <summary>
        /// Function that takes the <c>_probabilities</c> field and put
        /// each <c>key</c> the number of times the <c>value</c> in the <c>_eventTypesList</c> field.
        /// For examples: <c>{ DeadLightEvent, 3 }</c>, means that the list will have 3 <c>DeadLightEvent</c> in the list <c>_eventTypesList</c>.
        /// </summary>
        private void GenerateProbabilities()
        {
            // Generate a list with the probabilities
            foreach (var pair in _probabilities)
            {
                for (var i = 0; i < pair.Value; i++)
                {
                    _eventTypesList.Add(pair.Key);
                }
            }
        }
        
        private void Start()
        {
            _carController = GetComponent<CarController>();
            
            // This order must follow the CarEventType enumeration order.
            _carEvents.Add(gameObject.AddComponent<Types.DeadBatteryEvent>());
            _carEvents.Add(gameObject.AddComponent<Types.LightBreakEvent>());
            _carEvents.Add(gameObject.AddComponent<Types.FlatTireEvent>());
            _carEvents.Add(gameObject.AddComponent<Types.BrokenPartEvent>());

            GenerateProbabilities();

            StartCoroutine(EventLoop());
            StartCoroutine(BatteryHealing());
            StartCoroutine(BatteryConsumption());
            StartCoroutine(WheelConsumption());
        }
    }
}