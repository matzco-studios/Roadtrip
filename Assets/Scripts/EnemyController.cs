using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EnemyController : MonoBehaviour
{
    protected Transform Target;

    public float speed;
    public float hp = 100f;

    void Start() 
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public virtual void OnDeath() {
        Destroy(gameObject);
    }
    public virtual void OnHit(){
        Destroy(gameObject);
    }

    public void Hurt(float damage){
        hp -=damage;
        if (hp<0) 
            OnDeath(); 
        else 
            OnHit();
    }
}
