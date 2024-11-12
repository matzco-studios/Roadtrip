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
        if (player != null && carTrunk != null)
        {
            float distance = Vector3.Distance(player.transform.position, carTrunk.transform.position);
            if (distance > 10)
            {
                timeAway += Time.deltaTime;
                if (timeAway > 10)
                {
                    if (scorchlet == null)
                    {
                        truckPosition = carTrunk.transform.position;
                        spawnPositionDistance = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                        scorchlet = Instantiate(scorchletPrefab, truckPosition + spawnPositionDistance, Quaternion.identity);
                        anim = scorchlet.GetComponent<Animator>();
                    }
                    else
                    {
                        MoveScorchlet(scorchlet);
                        Debug.Log("Scorchlet is moving");
                    }
                }
            }
        }
    }
    void MoveScorchlet(GameObject enemy)
    {
        anim.SetInteger("moving", 1);
        // enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, player.transform.position, 1f * Time.deltaTime);
        // Transform objectTaken = scorchlet.transform.childCount > 0 ? scorchlet.transform.GetChild(0) : null;
        // if (objectTaken == null)
        // {
        //     enemy.transform.LookAt(carTrunk.transform);
        //     enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, carTrunk.transform.position, 5f * Time.deltaTime);
        // } else if (objectTaken != null)
        // {
        //     Debug.Log("Scorchlet has taken" + objectTaken.name);
        //     scorchlet.transform.position = Vector3.MoveTowards(scorchlet.transform.position, player.transform.position, 5f * Time.deltaTime);
        // }
    }
}
