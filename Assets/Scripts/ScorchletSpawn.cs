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
                        scorchlet = Instantiate(scorchletPrefab);
                    }
                    else
                    {
                        MoveScorchlet(scorchlet);
                    }
                }
            }
        }
    }
    void MoveScorchlet(GameObject enemy)
    {
        Transform objectTaken = scorchlet.transform.childCount > 0 ? scorchlet.transform.GetChild(0) : null;
        if (objectTaken == null)
        {
            enemy.transform.LookAt(player.transform);
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, carTrunk.transform.position, 5f * Time.deltaTime);
        } else if (objectTaken != null)
        {
            Debug.Log("Scorchlet has taken" + objectTaken.name);
            scorchlet.transform.position = Vector3.MoveTowards(scorchlet.transform.position, player.transform.position, 5f * Time.deltaTime);
        }
    }
}
