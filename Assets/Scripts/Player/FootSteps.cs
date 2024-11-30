using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FloorType
{
    GRASS, // 0
    WOOD,  // 1
    STONE  // 2 
}

public class FootSteps : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    
    void Start()
    {
        // You know the drill
    }

    void PickRandomAudioClip()
    {
        // Choose a random clip depend on the floor type
    }

    void Update()
    {
        // Play sound
    }
}
