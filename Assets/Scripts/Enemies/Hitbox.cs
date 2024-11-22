using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private float _damage = 35f;
    void OnTriggerEnter(Collider other)
    {
        print("Attack");
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ReduceHealth(_damage);
        }
    }
}
