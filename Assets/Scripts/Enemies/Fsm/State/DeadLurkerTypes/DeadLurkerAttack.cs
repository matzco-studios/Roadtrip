using UnityEngine;
using UnityEngine.AI;
using Enemies.Fsm.State.Types;

namespace Enemies.Fsm.State.DeadLurkerTypes
{
    public class DeadLurkerAttack : Attack
    {
        private float wait = 180f;
        public DeadLurkerAttack(EnemyController npc, NavMeshAgent agent, Animator anim, Transform player)
            : base(npc, agent, anim, player)
        {
            Name = StateType.Attack;
        }
        protected override void Enter()
        {
            Agent.isStopped = true;
            Stage = EventStage.Update;
            Anim.SetTrigger("StartAttack");
        }
        protected override void Update()
        {
            wait-= 1.0f;
            Anim.SetFloat("WalkSpeed", 0);
            Debug.Log(wait);
            Npc.transform.LookAt(Vector3.Scale(Player.position, new Vector3(1f, 0f, 1f)));

            if (wait<=0){
                Agent.isStopped =false;
                NextEnemyState = new DeadLurkerIdle(Npc, Agent, Anim, Player);
                Stage = EventStage.Exit;
                Debug.Log("Chase");
            }
        }
    }
}