using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EnemyController : MonoBehaviour
{
    private Transform target;

    public PostProcessVolume postProcess;
    public Animator animator;

    public float speed;
    public float hp = 100f;
    // public float activationDistance;

    private bool isWatched;

    void Start() 
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Make the enemy always look the player
        isWatched = GetComponent<Renderer>().isVisible;
        // Make the enemy run to the player if he's not looking at him
        if (!isWatched) 
        {
            Debug.Log("Enemy is invisible - Open your eyes");
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        } 
        animator.enabled = isWatched ? false : true;
        
        postProcess.weight = Mathf.Lerp(
            postProcess.weight, 
            (isWatched ? 1f : 0f), 
            speed * Time.deltaTime
        );

        // If user in activation zone
        /* if (Vector3.Distance(target.position, transform.position) < activationDistance) {

            // and is watched by enemy, add PostProcess effect of beingWatched
            postProcess.weight = Mathf.Lerp(
                postProcess.weight, 
                (isWatched ? 1f : 0f), 
                speed * Time.deltaTime
            );
        } */

    }

    public virtual void OnDeath(){
        Destroy(gameObject);
    }
    public virtual void OnHit(){
        //Destroy(gameObject);
    }

    public void Hurt(float damage){
        hp -=damage;
        if (hp<0){
            OnDeath();
        }else{
            OnHit();
        }
    }
}
