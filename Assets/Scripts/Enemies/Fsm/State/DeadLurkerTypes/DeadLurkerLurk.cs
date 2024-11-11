using Enemies.Fsm.State.Types;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Fsm.State.DeadLurkerTypes
{
    public class DeadLurkerLurk : Pursue
    {
        public DeadLurkerLurk(EnemyController npc, NavMeshAgent agent, Animator anim, Transform player)
            : base(npc, agent, anim, player)
        {
            Name = StateType.Pursue;
            Agent.speed = 5.0f;
            Agent.isStopped = false;
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
    }
}