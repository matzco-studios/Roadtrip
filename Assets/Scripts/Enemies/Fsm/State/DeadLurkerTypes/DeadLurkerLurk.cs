using Enemies.Fsm.State.Types;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Fsm.State.DeadLurkerTypes
{
    public class DeadLurkerLurk : Pursue
    {
        private Vector3 _posBehindPlayer = Vector3.back*5;
        private Vector3 _posLeftPlayer = Vector3.left*5;
        private Vector3 _posRightPlayer = Vector3.right*5;
        private Vector3 _targetPos;
        public DeadLurkerLurk(EnemyController npc, NavMeshAgent agent, Animator anim, Transform player)
            : base(npc, agent, anim, player)
        {
            Name = StateType.Pursue;
            Agent.speed = 5.0f;
            Agent.isStopped = false;
        }
        protected override void Enter()
        {
            _targetPos = _posBehindPlayer;
            base.Enter();
        }

        protected override void Update()
        {
            //Npc.transform.LookAt(Player);
            var pos = Player.position + Quaternion.AngleAxis(Player.eulerAngles.y, Player.up) * _targetPos;
            Agent.SetDestination(pos);
            Debug.DrawLine(Npc.transform.position, pos);
            Agent.speed = Mathf.Sqrt(Agent.remainingDistance*2)+0.25f;
            
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