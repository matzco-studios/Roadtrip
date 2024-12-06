using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Enemies.Scorchlet
{

    public class ScorchletController : EnemyController
    {
        private GameObject carTrunk;
        private GameObject player;
        private GameObject itemMemory;
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
        private float carDistance;
        
        public bool IsWatched => isWatched;

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
            carDistance = Vector3.Distance(transform.position, carTrunk.transform.position);
            if (!hasTakenObject && !isInTrunk && !isWatched)
            {
                float distance = Vector3.Distance(transform.position, carTrunk.transform.position);
                if (distance > 2.5f)
                {
                    MoveToTrunk();
                }
                else
                {
                    anim.SetInteger("moving", 0);
                    GetComponent<NavMeshAgent>().enabled = true;;
                    isInTrunk = true;
                }
            }
            if (isInTrunk && !isWatched)
            {
                if (carTrunk.transform.childCount > 0 && !hasTakenObject)
                {
                    GameObject item = carTrunk.transform.GetChild(Random.Range(0, carTrunk.transform.childCount)).gameObject;
                    TakeObject(item);
                    hasTakenObject = true;
                }
                anim.SetInteger("moving", 1);
                anim.speed = 0f;
                transform.LookAt(player.transform.position);
            }
            if (isWatched)
            {
                if (!isFleeing && playerDistance <= 7 && playerDistance > 2)
                {
                    Flee();
                    isFleeing = true;
                }
                if (isFleeing && playerDistance > 7)
                {
                    isFleeing = false;
                    isWatched = false;
                    Destroy(gameObject);
                }
            }
            if (carDistance > 20)
            {
                Destroy(gameObject);
            }
            if (playerDistance <= 2 && !isFleeing && !isWatched)
            {
                SceneManager.LoadScene("ScorchletJumpscare");
            }
        }
        void MoveToTrunk()
        {
            transform.LookAt(carTrunk.transform.position);
            anim.SetInteger("moving", 1);
        }
        void Flee()
        {
            oppositeDirection = player.transform.forward;
            screechingSound.Play();
            anim.SetInteger("moving", 1);
            anim.speed = fleeSpeed;
            agent.SetDestination(oppositeDirection);
        }
        public override void OnHit()
        {
            isFleeing = true;
            IsFlashed();
            Flee();
        }
        public override void OnDeath()
        {
            if (itemMemory)
            {
                itemMemory.transform.parent = null;
                itemMemory.GetComponent<Rigidbody>().isKinematic = false;
                itemMemory = null;
            }
            
        }

        void TakeObject(GameObject item)
        {
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.transform.SetParent(transform);
            item.transform.localPosition = new Vector3(0, 0.80f, 0.9f);
            itemMemory = item;
        }

    }
}