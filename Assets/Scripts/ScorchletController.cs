using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ScorchletController : MonoBehaviour
{
    private GameObject carTrunk;
    private GameObject player;
    private Animator anim;
    private bool hasTakenObject;
    private bool isInTrunk;
    private bool isWatched;
    private bool isFleeing;
    private float playerDistance;
    private float fleeSpeed = 15f;
    private Vector3 oppositeDirection;
    private NavMeshAgent agent;
    private AudioSource screechingSound;

    // Start is called before the first frame update
    void Start()
    {
        isFleeing = false;
        screechingSound = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        isWatched = false;
        carTrunk = GameObject.FindGameObjectWithTag("CarTrunk");
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame

    public void IsFlashed()
    {
        isWatched = true;
    }
    void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (!hasTakenObject && !isInTrunk && !isWatched)
        {
            float distance = Vector3.Distance(transform.position, carTrunk.transform.position);
            if (distance > 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, carTrunk.transform.position, 0.1f);
                transform.LookAt(carTrunk.transform.position);
                anim.SetInteger("moving", 1);
            }
            else
            {
                anim.SetInteger("moving", 0);
                isInTrunk = true;
            }
        }
        if (isInTrunk && !hasTakenObject)
        {
            if (carTrunk.transform.childCount > 0)
            {
                GameObject item = carTrunk.transform.GetChild(Random.Range(0, carTrunk.transform.childCount)).gameObject;
                item.GetComponent<Rigidbody>().isKinematic = true;
                item.transform.SetParent(transform);
                item.transform.localPosition = new Vector3(0, 0.80f, 0.9f);
                hasTakenObject = true;
            }
        }
        if (isWatched)
        {
            if (!isFleeing && playerDistance <= 7 && playerDistance > 2)
            {
                oppositeDirection = player.transform.forward;
                screechingSound.Play();
                Debug.Log("Scorchlet is fleeing");
                anim.SetInteger("moving", 1);
                anim.speed = fleeSpeed;
                agent.SetDestination(oppositeDirection);
                isFleeing = true;
            }
            if (isFleeing && playerDistance > 7)
            {
                isFleeing = false;
                isWatched = false;
                Destroy(gameObject);
            }
        }
        if (playerDistance <= 2 && !isFleeing && !isWatched)
        {
            transform.LookAt(player.transform.position);
            SceneManager.LoadScene("ScorchletJumpscare");
        }
    }
}
