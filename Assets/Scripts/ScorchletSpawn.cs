using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScorchletSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject carTrunk;
    public GameObject scorchletPrefab;
    private GameObject scorchlet;
    private float timeAway;
    private Vector3 truckPosition;
    private Vector3 spawnPositionDistance;
    private Animator anim;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, carTrunk.transform.position);
        if (distance > 10 && scorchlet == null)
        {
            timeAway += Time.deltaTime;
            Debug.Log(timeAway);
            if (timeAway > 10)
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
        spawnPositionDistance = new Vector3(UnityEngine.Random.Range(-5, 5), 0, UnityEngine.Random.Range(-5, 5));
        scorchlet = Instantiate(scorchletPrefab, new Vector3(truckPosition.x + spawnPositionDistance.x, 0, truckPosition.z + spawnPositionDistance.z), Quaternion.identity);
        anim = scorchlet.GetComponent<Animator>();
    }
}
