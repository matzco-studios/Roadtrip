using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Fsm.State
{
    public class EnemyState
    {
        public StateType Name;
        protected EventStage Stage;
        protected EnemyController Npc;
        protected Animator Anim;
        protected Transform Player;
        protected EnemyState NextEnemyState;
        protected NavMeshAgent Agent;

        protected EnemyState(EnemyController npc, NavMeshAgent agent, Animator anim, Transform player)
        {
            Name = StateType.None;
            Npc = npc;
            Agent = agent;
            Anim = anim;
            Player = player;
            Stage = EventStage.Enter;
        }

        protected virtual void Enter()
        {
            Stage = EventStage.Update;
        }

        protected virtual void Update()
        {
            Stage = EventStage.Update;
        }

        protected virtual void Exit()
        {
            Stage = EventStage.Exit;
        }

        public EnemyState Process()
        {
            if (Stage == EventStage.Enter) Enter();
            if (Stage == EventStage.Update) Update();
            if (Stage == EventStage.Exit)
            {
                Exit();
                return NextEnemyState;
            }

            return this;
        }
    }
}