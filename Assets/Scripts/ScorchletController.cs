using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScorchletController : MonoBehaviour
{
    private GameObject carTrunk;
    private GameObject player;
    private Animator anim;
    private bool hasTakenObject;
    private bool isInTrunk;
    private bool hasJumped;
    private bool canJump;
    private bool isWatched;
    private float playerDistance;

    // public void Jump()
    // {
    //     if (anim.GetInteger("moving") != 16)
    //     {
    //         anim.SetInteger("moving", 16);
    //         transform.position += new Vector3(2, 5, 0);
    //         Debug.Log("Scorchlet has jumped");
    //     }
    // }
    // Start is called before the first frame update
    void Start()
    {
        isWatched = false;
        canJump = false;
        hasJumped = false;
        carTrunk = GameObject.FindGameObjectWithTag("CarTrunk");
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {

        // if (other.CompareTag("JumpingZone"))
        // {
        //     anim.SetInteger("battle", 1);
        //     canJump = true;
        // }
    }

    // void OnTriggerStay(Collider other)
    // {
    //     if (other.CompareTag("JumpingZone") && canJump)
    //     {
    //         Jump();
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        isWatched = gameObject.GetComponent<Renderer>().isVisible;
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (!hasTakenObject && !isInTrunk)
        {
            float distance = Vector3.Distance(transform.position, carTrunk.transform.position);
            if (distance > 2f)
            {
                transform.LookAt(carTrunk.transform.position);
                anim.SetInteger("moving", 1);
                transform.position = Vector3.MoveTowards(transform.position, carTrunk.transform.position, 2f);
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
        if ((isWatched && playerDistance <= 7) || hasTakenObject)
        {
            Debug.Log("Scorchlet is fleeing");
            anim.SetInteger("moving", 1);
            Vector3 randomDirection = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            transform.LookAt(randomDirection);
            transform.position = Vector3.MoveTowards(transform.position, randomDirection, 10f);
        }
    }
}
