using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScorchletController : MonoBehaviour
{
    private GameObject carTrunk;
    private Animator anim;
    private bool hasTakenObject;
    private bool isInTrunk;
    // Start is called before the first frame update
    void Start()
    {
        carTrunk = GameObject.FindGameObjectWithTag("FuelTank");
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Scorchlet has collided with " + other.gameObject.tag);
        if (other.CompareTag("GrabbableItem") && isInTrunk)
        {
            Transform parent = other.gameObject.transform.parent;
            if (parent == null || !parent.CompareTag("Scorchlet") || !parent.CompareTag("Player"))
            {
                other.gameObject.transform.SetParent(transform);
                other.gameObject.transform.localPosition = new Vector3(1.25f, -0.15f, -0.26f);
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                hasTakenObject = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasTakenObject && !isInTrunk)
        {
            Debug.Log("Scorchlet is moving towards the car trunk");
            float distance = Vector3.Distance(transform.position, carTrunk.transform.position);
            Debug.Log(distance);
            if (distance > 0.5f)
            {
                transform.LookAt(carTrunk.transform);
                anim.SetInteger("moving", 1);
            }
            else
            {
                isInTrunk = true;
                anim.SetInteger("moving", 0);
            }
        }
        else if (isInTrunk && hasTakenObject)
        {
            Debug.Log("Scorchlet has taken object and is in trunk");
            anim.SetInteger("moving", 0);
        }
    }
}
