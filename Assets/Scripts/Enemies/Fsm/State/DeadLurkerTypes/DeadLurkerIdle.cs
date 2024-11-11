using UnityEngine;
using UnityEngine.AI;
using Enemies.Fsm.State.Types;

namespace Enemies.Fsm.State.DeadLurkerTypes
{
    public class DeadLurkerIdle : Idle
    {
        public DeadLurkerIdle(EnemyController npc, NavMeshAgent agent, Animator anim, Transform player)
            : base(npc, agent, anim, player)
        {
            Name = StateType.Idle;
        }
        protected override void Update()
        {
            
        }
    }
}