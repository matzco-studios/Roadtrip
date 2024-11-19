using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScorchletSpawn : MonoBehaviour
{
    private GameObject player;
    private GameObject carTrunk;
    public GameObject scorchletPrefab;
    private GameObject scorchlet;
    private float timeAway;
    private Vector3 truckPosition;
    private Vector3 spawnPositionDistance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        carTrunk = GameObject.FindGameObjectWithTag("CarTrunk");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, carTrunk.transform.position);
        if (distance > 10 && scorchlet == null)
        {
            timeAway += Time.deltaTime;
            if (timeAway > 2) // set time away
            {
                int random = UnityEngine.Random.Range(0, 1); // Set spawn chance
                if (random == 0)
                {
                    SpawnScorchlet();
                    timeAway = 0;
                }
                else
                {
                    timeAway = 0;
                }
            }
        }
        else if (distance <= 10)
        {
            timeAway = 0;
        }
    }

    void SpawnScorchlet()
    {
        truckPosition = carTrunk.transform.position;
        spawnPositionDistance = new Vector3(UnityEngine.Random.Range(-4, 4), 0, UnityEngine.Random.Range(-4, 4));
        Vector3 spawnPosition = new Vector3(truckPosition.x + spawnPositionDistance.x, 0, truckPosition.z + spawnPositionDistance.z);
        if (IsOverlapping(spawnPosition))
        {
            SpawnScorchlet();
        }
        else
        {
            scorchlet = Instantiate(scorchletPrefab, spawnPosition, Quaternion.identity);
        }
    }

    bool IsOverlapping(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 1);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].CompareTag("Scorchlet"))
            {
                return true;
            }
            i++;
        }
        return false;
    }
}
