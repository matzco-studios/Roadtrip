using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryEnemyController : MonoBehaviour
{
    private Transform target;
    public float speed;

    void Start() 
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Make the enemy always look the player
        transform.LookAt(target);

        // Make the enemy run to the player if he's not looking at him
        if (!GetComponent<Renderer>().isVisible)
        {
            Debug.Log("Enemy is invisible - Open your eyes");
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
