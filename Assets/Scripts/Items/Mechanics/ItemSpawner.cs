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
        private GameObject _fuelTank;
        
        private CarController _car;
        private Transform     _playerPosition;
        
        // Spawn Parameters
        public float spawnRadius;
        public int   spawnDelay;

        // Spawnable Items
        private Queue<GameObject> _priorItems = new();
        public  GameObject[]      items;

        private static bool instaSpawn = false;

        private GameObject FindPriorInBonus(string name) => 
            items.First(i => i.name.Equals(name));

        private List<GameObject> spawnList = new();

        public static void SetSpawn() => instaSpawn = true;
        
        private void Awake()
        {
            _playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            _car            = GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
            
            _akkum    = FindPriorInBonus("Akkum");
            _carLight = FindPriorInBonus("CarLight");
            _fuelTank = FindPriorInBonus("FuelTank");
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
            if ((!_car.IsBatteryInside() || _car.Battery.IsDead()) && !_priorItems.Contains(_akkum)){
                _priorItems.Enqueue(_akkum);
            }
            // CarLight
            else if (_car.carLights.FindAll(l => !l.IsWorking).Count >= 2 && !_priorItems.Contains(_carLight))
            {
                _priorItems.Enqueue(_carLight);
                Debug.Log(_car.carLights.FindAll(l => !l.IsWorking).Count());
            }
            // FuelTank
            else if (_car.currentFuel < Random.Range(10, 20) && !_priorItems.Contains(_fuelTank))
            {
                _priorItems.Enqueue(_fuelTank);
                Debug.Log(_car.carLights.FindAll(l => !l.IsWorking).Count());
            }
            // Bonus
            else
                // Pick a random item from the bonusItems list
                _priorItems.Enqueue(items[Random.Range(0, items.Length)]);
        }

        private void SpawnNow(){
            var dist = Random.insideUnitSphere;
            dist /= dist.magnitude;
            var spawnPos = _playerPosition.position + (dist * spawnRadius*0.7f);
            spawnPos.y = _playerPosition.position.y+ 35;
            
            if (_car.IsPlayerInside){
                var iSpawn = Instantiate(_priorItems.Peek(), spawnPos, Quaternion.identity);
                iSpawn.GetComponent<Rigidbody>().isKinematic = false;
                spawnList.Add(iSpawn);
                _priorItems.Dequeue();
            }

            GameObject obj = null;
            foreach (GameObject itm in spawnList){
                if (Vector3.Distance(_car.transform.position, itm.transform.position)>spawnRadius*3){
                    Destroy(itm); obj = itm; break;
                }
            }

            if (obj){
                spawnList.Remove(obj);
            }
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
                yield return new WaitForSeconds(spawnDelay);
                CheckPlayerNeeds();
                
                SpawnNow();
            }
        }

        /// <summary>
        /// You know the drill
        /// </summary>
        private void Start()
        {
            StartCoroutine(ItemSpawn());
        }

        void Update(){
            if (instaSpawn) {
                instaSpawn = false;
                _priorItems.Enqueue(_akkum);
                SpawnNow();
            }
        }
    }
}
