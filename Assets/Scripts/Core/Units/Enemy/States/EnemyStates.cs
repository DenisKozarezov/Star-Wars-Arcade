using UnityEngine;
using Core.Models;

namespace Core.Units
{
    public abstract class EnemyBaseState : IState<EnemyView>
    {
        protected readonly IStateMachine<EnemyView> StateMachine;
        protected EnemyView Context => StateMachine.Context;

        public EnemyBaseState(IStateMachine<EnemyView> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }

    public class EnemyPatrolState : EnemyBaseState
    {
        private Vector2 _initPosition;
        private Vector3 _target;
        private readonly float _velocity;
        private readonly float _rotationSpeed;

        public EnemyPatrolState(IStateMachine<EnemyView> stateMachine, EnemySettings settings) : base(stateMachine) 
        {
            _velocity = settings.EnemyModel.Velocity;
            _rotationSpeed = settings.EnemyModel.RotationSpeed;
        }

        private Vector3 GetRandomDestination()
        {
            return _initPosition + Random.insideUnitCircle * 2f;
        }
        private bool HasReachedDestination()
        {
            return (_target - Context.transform.position).sqrMagnitude <= 1f;
        }

        public override void Enter()
        {
            _initPosition = Context.transform.position;
            _target = GetRandomDestination();
        }
        public override void Exit()
        {

        }
        public override void Update()
        {
            if (!HasReachedDestination())
            {
                Vector2 direction = _target - Context.transform.position;
                float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Context.transform.rotation = Quaternion.Slerp(Context.transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
                Context.transform.Translate(direction * _velocity * Time.deltaTime);
            }
            else _target = GetRandomDestination();
        }
    }
    public class EnemySeekState : EnemyBaseState
    {
        private readonly PlayerView _player;

        public EnemySeekState(IStateMachine<EnemyView> stateMachine) : base(stateMachine)
        {
            //_player = player;
        }

        private Vector3 GetDirection()
        {
            if (_player == null) return Vector3.zero;

            return _player.transform.position - Context.transform.position;
        }
        public override void Enter()
        {
     
        }
        public override void Exit()
        {
           
        }
        public override void Update()
        {
            Vector3 direction = GetDirection();
            Context.transform.position += direction * Time.deltaTime;
        }
    }
    public class EnemyAttackState : EnemyBaseState
    {
        public EnemyAttackState(IStateMachine<EnemyView> stateMachine) : base(stateMachine)
        {

        }
        public override void Enter()
        {

        }
        public override void Exit()
        {

        }
        public override void Update()
        {

        }
    }
}