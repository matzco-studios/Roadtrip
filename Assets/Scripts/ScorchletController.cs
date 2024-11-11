using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScorchletController : MonoBehaviour
{
    public GameObject player;
    public GameObject carTrunk;
    public GameObject scorchletPrefab;
    private GameObject scorchlet;
    private float timeAway;
    // Start is called before the first frame update
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
                Debug.Log(timeAway);
                if (timeAway > 10)
                {
                    if (scorchlet == null)
                    {
                        scorchlet = Instantiate(scorchletPrefab);
                    }
                    else
                    {
                        GoToCar(scorchlet);
                    }
                }
            }
        }
    }
    void GoToCar(GameObject enemy)
    {
        enemy.transform.LookAt(player.transform);
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, carTrunk.transform.position, 5f * Time.deltaTime);
    }
}
