using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Fsm.State.Types
{
    public class Attack : EnemyState
    {
        public Attack(EnemyController npc, NavMeshAgent agent, Animator anim, Transform player)
            : base(npc, agent, anim, player)
        {
            Name = StateType.Attack;
        }

        protected override void Enter()
        {
            Anim.SetTrigger("attack");
            Agent.isStopped = true;
            base.Enter();
        }

        protected override void Update()
        {
            /*Npc.transform.LookAt(Player);
            Npc.AttackPlayer();
            
            if (!Npc.CanAttackPlayer)
            {
                if (Npc.CanSeePlayer)
                {
                    NextEnemyState = new Pursue(Npc, Agent, Anim, Player);
                }
                else
                {
                    NextEnemyState = new Idle(Npc, Agent, Anim, Player);
                }
                
                Stage = EventStage.Exit;
            }*/
        }

        protected override void Exit()
        {
            Anim.ResetTrigger("attack");
            base.Exit();
        }
    }
}