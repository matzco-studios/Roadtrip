using System;
using Enemies.Fsm.State.Types;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Fsm.State.DeadLurkerTypes
{
    public class DeadLurkerLurk : Pursue
    {
        private Vector3 _posBehindPlayer = Vector3.back*4.45f;
        private Vector3 _posLeftPlayer = Vector3.left*8;
        private Vector3 _posRightPlayer = Vector3.right*8;
        private Vector3 _targetPos;
        private float _runAwaySpeed = 8.4f;
        private Renderer _renderer;
        public DeadLurkerLurk(EnemyController npc, NavMeshAgent agent, Animator anim, Transform player)
            : base(npc, agent, anim, player)
        {
            Name = StateType.Pursue;
            Agent.speed = 5.0f;
            Agent.isStopped = false;
        }

        private void LookAtPlayer()
        {
            Npc.transform.LookAt(Vector3.Scale(Player.position, new Vector3(1f, 0f, 1f)));
        }

        protected override void Enter()
        {
            _renderer = Npc.GetComponentInChildren<Renderer>();
            _targetPos = _posBehindPlayer;
            Anim.SetBool("IsMoving", true);
            base.Enter();
        }

        protected override void Update()
        {
            Vector3 pos = Player.position + Quaternion.AngleAxis(Player.eulerAngles.y, Player.up) * _targetPos;
            
            _targetPos = _posBehindPlayer;
            var spd = Mathf.Sqrt(Agent.remainingDistance*2.2f)+0.65f;
            Agent.speed = (spd == float.PositiveInfinity) ? 0.65f : spd;
            LookAtPlayer();

            if (_renderer.isVisible) {
                Agent.speed += _runAwaySpeed;
            }
            
            Agent.SetDestination(pos);
            Debug.DrawLine(Npc.transform.position, pos);

            if (Vector3.Distance(Player.position, Npc.transform.position)<3f) {
                LookAtPlayer();
                Agent.SetDestination(Player.position);
                NextEnemyState = new DeadLurkerAttack(Npc, Agent, Anim, Player);
                Stage = EventStage.Exit;
                Debug.Log("Attack now");
            }

            Anim.SetFloat("WalkSpeed", Agent.velocity.magnitude);

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