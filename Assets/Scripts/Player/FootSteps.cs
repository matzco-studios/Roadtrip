using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FloorType
{
    GRASS, // 0
    WOOD,  // 1
    STONE, // 2
}

public class FootSteps : MonoBehaviour
{
    #region Members

    [SerializeField] private AudioClip[] _clips;
    
    #endregion

    #region Properties
    
    // Here goes all the properties
    
    #endregion

    #region CustomMethods

    /// <summary>
    ///     Pick a random audio clip depending on the floor type
    /// 
    ///     Ex: play step stone clip if player is walking on concrete
    /// 
    ///     (Randomness because having multiples sonorities is better for immersion)
    /// </summary>
    /// <returns>TheAudioClip</returns>
    private AudioClip PickRandomAudioClip()
    {
        // Choose a random audio clip;
        return null;
    }
    
    #endregion

    #region UnityGameMethods

    void Start()
    {
    }

    void Update()
    {
        // Play the audio
    }
    
    #endregion
}
