using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCamAnimation : MonoBehaviour
{
    public GameObject firstCamera;
    public GameObject player;
    private Animator _camAnim;
    private bool _animtionPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        _camAnim = firstCamera.GetComponent<Animator>();;
        _camAnim.SetTrigger("PlayJenkins");
    }

    // Update is called once per frame
    void Update()
    {
        if (_camAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            _camAnim.SetTrigger("PlayJenkins");
            firstCamera.SetActive(false);
            player.SetActive(true);
            player.transform.rotation = Quaternion.Euler(0, -450, 0);
        }
    }
}
