using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Enemies.Scorchlet
{
    public class ScorchletJumpscare : MonoBehaviour
    {
        private Animator anim;
        private float timer = 0;
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
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f && timer==0)
            { timer = 1; Destroy(gameObject.transform.GetChild(1).gameObject); }

            if (timer>0){ timer += Time.deltaTime; }

            if (timer>3){ SceneManager.LoadScene(0); }
        }
    }
}
