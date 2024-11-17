using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Fsm.State.Types
{
    public class Pursue : EnemyState
    {
        public Pursue(EnemyController npc, NavMeshAgent agent, Animator anim, Transform player)
            : base(npc, agent, anim, player)
        {
            Name = StateType.Pursue;
            Agent.speed = 5.0f;
            Agent.isStopped = false;
        }

        protected override void Enter()
        {
            Anim.SetTrigger("walk");
            base.Enter();
        }

        protected override void Update()
        {
            Npc.transform.LookAt(Player);
            Agent.SetDestination(Player.position);
            
            /*if (Npc.CanAttackPlayer)
            {
                NextEnemyState = new Attack(Npc, Agent, Anim, Player);
                Stage = EventStage.Exit;
            }
            
            if (!Agent.hasPath && !Npc.CanSeePlayer)
            {
                NextEnemyState = new Idle(Npc, Agent, Anim, Player);
                Stage = EventStage.Exit;
            }*/
        }

        protected override void Exit()
        {
            Anim.ResetTrigger("walk");
            base.Exit();
        }
    }
}