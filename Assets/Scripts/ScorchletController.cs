using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorchletController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GrabbableItem")
        {
            Transform parent = other.gameObject.transform.parent;
            if (parent == null || parent.CompareTag("Scorchlet") == false || parent.CompareTag("Player") == false)
            {
                other.gameObject.transform.SetParent(transform);
                other.gameObject.transform.localPosition = new Vector3(1.25f, -0.15f, -0.26f);
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
