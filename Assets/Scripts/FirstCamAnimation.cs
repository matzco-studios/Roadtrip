using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FirstCamAnimation : MonoBehaviour
{
    public GameObject firstCamera;
    public GameObject player;
    
    private Animator _camAnim;
    
    void Start()
    {
        player.SetActive(false);
        _camAnim = firstCamera.GetComponent<Animator>();
        _camAnim.SetTrigger("PlayJenkins");
    }

    // Update is called once per frame
    void Update()
    {
        if (_camAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            firstCamera.SetActive(false);
            player.SetActive(true);
            player.transform.rotation = Quaternion.Euler(0, -450, 0);
        }
    }
}
