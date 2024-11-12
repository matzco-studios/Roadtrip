using UnityEngine;
using UnityEngine.AI;
using Enemies.Fsm.State.Types;

namespace Enemies.Fsm.State.DeadLurkerTypes
{
    public class DeadLurkerAttack : Attack
    {
        private float wait = 160f;
        public DeadLurkerAttack(EnemyController npc, NavMeshAgent agent, Animator anim, Transform player)
            : base(npc, agent, anim, player)
        {
            Name = StateType.Attack;
        }
        protected override void Enter()
        {
            Agent.SetDestination(Player.position);
            Agent.speed = 3f;
            Stage = EventStage.Update;
        }
        protected override void Update()
        {
            if (Agent.remainingDistance<3f && !Agent.isStopped)
            {
                Anim.SetTrigger("StartAttack");
                Agent.isStopped = true;
                Agent.speed = 0;
                Agent.ResetPath();
                Anim.SetBool("IsMoving",false);
                wait--;
            }else if (wait<160) {wait-= 1.0f;}
            Anim.SetFloat("WalkSpeed", Agent.velocity.magnitude);
            Debug.Log(wait);
            Npc.transform.LookAt(Vector3.Scale(Player.position, new Vector3(1f, 0f, 1f)));

            if (wait<=0){
                NextEnemyState = new DeadLurkerIdle(Npc, Agent, Anim, Player);
                Stage = EventStage.Exit;
                Debug.Log("Chase");
            }
        }
    }
}