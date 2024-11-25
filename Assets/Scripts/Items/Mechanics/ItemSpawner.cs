using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Car;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items.Mechanics
{
    public class ItemSpawner : MonoBehaviour
    {
        private GameObject _akkum;
        private GameObject _carLight;
        // private GameObject _fuelTank;
        
        private CarController _car;
        private Transform     _playerPosition;
        
        // Spawn Parameters
        public float spawnRadius;
        public int   spawnDelay;

        // Spawnable Items
        private Queue<GameObject> _priorItems = new();
        public  GameObject[]      items;

        private GameObject FindPriorInBonus(string name) => 
            items.First(i => i.name.Equals(name));
        
        private void Awake()
        {
            _playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            _car            = GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
            
            _akkum    = FindPriorInBonus("Akkum");
            _carLight = FindPriorInBonus("CarLight");
            // _fuelTank = FindPriorInBonus("FuelTank"); // NOT HERE YET
        }

        private void CheckPlayerNeeds()
        {
            /*
             * - Check if akkum needed
             * - Check how many CarLights are missing
             * - Check if car fuel will be empty soon
             * if player do it good, can prioritize bonus items Ex: Flashlight or Music Box
             */
            
            // Fuel
            // if (_car.currentFuel <= 19 && !_priorItems.Contains(_fuelTank)) 
            //     _priorItems.Enqueue(_fuelTank); // Max fuel value is 38, enqueue when it's at half
            
            // Akkum 
            if (!_car.IsBatteryInside() && !_priorItems.Contains(_akkum))
                _priorItems.Enqueue(_akkum);
            
            // CarLight
            else if (_car.carLights.FindAll(l => !l.IsWorking).Count >= 2 && !_priorItems.Contains(_carLight))
            {
                _priorItems.Enqueue(_carLight);
                Debug.Log(_car.carLights.FindAll(l => !l.IsWorking).Count());
            }
            
            // Bonus
            else
                // Pick a random item from the bonusItems list
                _priorItems.Enqueue(items[Random.Range(0, items.Length)]);
        }
        
        /// <summary>
        /// This method is called every <b>5 seconds</b>.
        /// It spawns <b>the first item in the prioritySpawnableItems</b> on a radius that use the player position as origin.
        /// The radius value is specified in the  <b>Unity Inspector</b>
        /// </summary>
        private IEnumerator ItemSpawn()
        {
            // Spawn Item Continuously each 5 seconds
            while (true)
            {
                var spawnPos = _playerPosition.position + Random.insideUnitSphere * spawnRadius;
                
                yield return new WaitForSeconds(spawnDelay);
                CheckPlayerNeeds();
                
                Instantiate(_priorItems.Peek(), new Vector3(spawnPos.x, _playerPosition.position.y+.2f, spawnPos.z), Quaternion.identity);
                _priorItems.Dequeue();
            }
        }

        /// <summary>
        /// You know the drill
        /// </summary>
        private void Start()
        {
            StartCoroutine(ItemSpawn());
        }
    }
}
