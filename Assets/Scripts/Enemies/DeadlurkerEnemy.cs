using UnityEngine;
using UnityEngine.AI;
using Enemies.Fsm.State;
using Enemies.Fsm.State.Types;
using Enemies.Fsm.State.DeadLurkerTypes;

public class DeadlurkerEnemy : EnemyController
{
    private NavMeshAgent agent;
    private Animator anim;
    private EnemyState currentState;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = new DeadLurkerIdle(this, agent, anim, player);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState!=null)
            currentState = currentState.Process();
    }

    public override void OnDeath(){
        anim.SetTrigger("Death");
        currentState = null;
        agent.enabled = false;
    }

    public override void OnHit(){
        anim.SetTrigger("Hit");
    }
}
