using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Fsm.State.Types
{
    public class Idle : EnemyState
    {
        public Idle(EnemyController npc, NavMeshAgent agent, Animator anim, Transform player)
            : base(npc, agent, anim, player)
        {
            Name = StateType.Idle;
        }

        protected override void Enter()
        {
            Anim.SetTrigger("idle");
            base.Enter();
        }

        protected override void Update()
        {
            /*if (Npc.CanSeePlayer)
            {
                if (Npc.CanAttackPlayer)
                {
                    NextEnemyState = new Attack(Npc, Agent, Anim, Player);
                }
                else
                {
                    NextEnemyState = new Pursue(Npc, Agent, Anim, Player);
                }

                Stage = EventStage.Exit;
            }*/
        }

        protected override void Exit()
        {
            Anim.ResetTrigger("idle");
            base.Exit();
        }
    }
}