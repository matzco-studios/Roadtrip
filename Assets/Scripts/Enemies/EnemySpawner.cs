using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float _sunburnedDespawn = 140, _deadlurkerDespawn = 80;
    private Transform _player;

    private bool IsInDistance(Transform enemy, float dist)
    {
        return Vector3.Distance(enemy.position, _player.position)<dist;
    }
    IEnumerator DeadlurkerManager() 
    {
        while (true)
        {
            var timer = Random.Range(6, 16);
            print($"Event scheduled in {timer} seconds.");
            yield return new WaitForSeconds(timer);
            
            if (_deadlurkerEnemies.Count<_deadlurkerMax)
            {
                if (Random.Range(0f, 10f)<_deadlurkerChance) {
                    _deadlurkerChance -= Random.Range(1.5f, 3.2f);
                    // SPAWN ENEMY AND ADD TO LIST
                } else {
                    _deadlurkerChance++;
                }
            }
        }
    }

    void EnemyDeleter(List<GameObject> enemyList, float despawnDist) 
    {
        GameObject objToDelete = null;
        foreach(GameObject obj in enemyList){
            if (!IsInDistance(obj.transform, despawnDist)){
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
        StartCoroutine(DeadlurkerManager());
    }

    void LateUpdate(){
        EnemyDeleter( _sunburnedEnemies,  _sunburnedDespawn);
        EnemyDeleter(_deadlurkerEnemies, _deadlurkerDespawn);
    }
}
