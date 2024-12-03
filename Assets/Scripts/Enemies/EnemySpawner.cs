using System.Collections;
using System.Collections.Generic;
using Car;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    private List<GameObject> _sunburnedEnemies = new();
    private List<GameObject> _deadlurkerEnemies = new();

    [SerializeField]
    private GameObject _sunburnedPrefab, _deadlurkerPrefab;
    [SerializeField]
    private int _sunburnedMax = 2, _deadlurkerMax = 3;
    [SerializeField]
    private float _sunburnedChance = 1, _deadlurkerChance = 1;

    [SerializeField]
    private float _sunburnedDespawn = 140, _deadlurkerDespawn = 140;
    private Transform _player;
    private CarController _car;

    private bool IsInDistance(Transform enemy, float dist)
    {
        return Vector3.Distance(enemy.position, _player.position)<dist;
    }
    IEnumerator EnemyManager(float minT, float maxT, 
        List<GameObject> enemyList, GameObject prefab, 
        int max, float chance, float dist) 
    {
        while (true)
        {
            var timer = Random.Range(minT, maxT);
            print($"Spawn in {timer} seconds!!!!");
            yield return new WaitForSeconds(timer);
            
            if (enemyList.Count<max)
            {
                if (Random.Range(0f, 10f)<chance) {
                    chance -= Random.Range(1.5f, 3.2f);
                    var spawnPoint = Random.Range(dist*0.6f, dist*0.8f);
                    var pos = _player.position - _player.forward*spawnPoint;
                    print(pos);
                    if(NavMesh.SamplePosition(pos, out NavMeshHit hit, 6, NavMesh.AllAreas)){
                        GameObject obj = Instantiate(prefab, hit.position, Quaternion.Euler(Vector3.zero), null);
                        enemyList.Add(obj);

                        var agent = obj.GetComponent<NavMeshAgent>();
                        
                        print("SPAWN " + obj + " AT " + obj.transform.position);
                    }else{
                        print("Couldnt spawn!");
                        print(hit.position);
                    }
                } else {
                    chance++;
                }
            }
        }
    }

    void EnemyDeleter(List<GameObject> enemyList, float despawnDist) 
    {
        GameObject objToDelete = null;
        foreach(GameObject obj in enemyList){
            bool isFarAwayEnough = !IsInDistance(obj.transform, despawnDist/2);
            bool isFarAwayEnoughLonger = !IsInDistance(obj.transform, despawnDist*0.7f);
            if (isFarAwayEnoughLonger && _car.IsPlayerInside || 
                (obj.GetComponent<EnemyController>().hp<=0 && 
                    !obj.GetComponentInChildren<Renderer>().isVisible &&
                    isFarAwayEnough)){
                objToDelete = obj; break;
            }
        }
        if (objToDelete){
            Destroy(objToDelete);
            enemyList.Remove(objToDelete);
        }
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _car = GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
        StartCoroutine(EnemyManager(6, 15, _deadlurkerEnemies, _deadlurkerPrefab, _deadlurkerMax, _deadlurkerChance, _deadlurkerDespawn));
        StartCoroutine(EnemyManager(8, 24, _sunburnedEnemies, _sunburnedPrefab, _sunburnedMax, _sunburnedChance, _sunburnedDespawn));
    }

    void LateUpdate(){
        EnemyDeleter( _sunburnedEnemies,  _sunburnedDespawn);
        EnemyDeleter(_deadlurkerEnemies, _deadlurkerDespawn);
    }
}
