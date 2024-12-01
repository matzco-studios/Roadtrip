using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorchletJumpscare : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0.75f;
        anim.SetInteger("battle", 1);
        anim.SetInteger("moving", 7);
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.85f)
        {
            Destroy(gameObject);
        }
    }
}
