using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Serialization;

public class FootSteps : MonoBehaviour
{
    #region Members

    [SerializeField] 
    private AudioClip[] clips;
    private AudioSource _audio;

    [Range(0f, 1f)] [SerializeField]
    private float rate;
    private float _coolDown;
    
    #endregion

    #region Properties
    
    // Here goes all the properties
    
    #endregion

    #region CustomMethods

    /// <summary>
    ///     Pick a random audio clip
    /// 
    ///     Ex: play step clip2 if player is walking
    ///
    ///     TODO : Add Clips for Walk on Road (On concrete)
    ///     (Randomness because having multiples sonorities is better for immersion)
    /// </summary>
    /// <returns>TheChosenAudioClip</returns>
    private AudioClip PickRandomAudioClip() => 
        clips[Random.Range(0, clips.Length)];
    
    #endregion

    #region UnityGameMethods

    private void Awake() => _audio = GetComponent<AudioSource>();

    private void Update()
    {
        _coolDown -= Time.deltaTime;
        
        if (_coolDown < 0 && PlayerController.IsWalking)
        {
            _audio.PlayOneShot(PickRandomAudioClip()); 
            _coolDown = rate;
        }
    }
    
    #endregion
}
