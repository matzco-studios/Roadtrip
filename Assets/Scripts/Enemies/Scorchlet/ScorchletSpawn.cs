using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Scorchlet
{
    public class ScorchletSpawn : MonoBehaviour
    {
        private GameObject player;
        private GameObject carTrunk;
        public GameObject scorchletPrefab;
        private GameObject scorchlet;
        private float timeAway;
        private Vector3 truckPosition;
        private Vector3 spawnPositionDistance;
        public float carLength = 5.4f;
        public float carWidth = 0.5f;

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
                if (timeAway > 10) // FOR DEBUG, CHANGE TO 2
                {
                    int random = UnityEngine.Random.Range(0, 3); // FOR DEBUG, CHANGE TO 1
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
            spawnPositionDistance = new Vector3(UnityEngine.Random.Range(-3, 3), 0, UnityEngine.Random.Range(-3, -6));
            Vector3 spawnPosition = new Vector3(truckPosition.x + spawnPositionDistance.x, carTrunk.transform.position.y, truckPosition.z + spawnPositionDistance.z);
            scorchlet = Instantiate(scorchletPrefab, spawnPosition, Quaternion.identity);
            scorchlet.GetComponent<NavMeshAgent>().enabled = false;
        }
    }
}