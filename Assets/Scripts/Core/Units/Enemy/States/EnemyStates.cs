using UnityEngine;

namespace Core.Units
{
    public abstract class EnemyBaseState : IState<EnemyController>
    {
        protected readonly IStateMachine<EnemyController> StateMachine;
        protected EnemyController Context => StateMachine.Context;
        protected ITransformable Transformable => Context.Transformable;

        protected EnemyBaseState(IStateMachine<EnemyController> stateMachine)
        {
            StateMachine = stateMachine;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }

    public class EnemyPatrolState : EnemyBaseState
    {
        private readonly float _velocity;
        private readonly float _rotationSpeed;
        private readonly float _deacceleration;
        private readonly float _patrolRadius;
        private Vector2 _initPosition;
        private Vector2 _destination;
        private Vector2 _currentPosition;

        public EnemyPatrolState(IStateMachine<EnemyController> stateMachine, EnemyModel model) : base(stateMachine) 
        {
            _velocity = model.Velocity;
            _rotationSpeed = model.RotationSpeed;
            _deacceleration = model.Deacceleration;
            _patrolRadius = model.PatrolRadius;
        }

        private Vector2 GetRandomDestination()
        {
            return _initPosition + Random.insideUnitCircle * _patrolRadius;
        }
        private bool HasReachedDestination()
        {
            return (_destination - Transformable.Position).sqrMagnitude <= 1f;
        }

        public override void Enter()
        {
            _initPosition = Transformable.Position;
            _destination = GetRandomDestination();
        }
        public override void Exit()
        {

        }
        public override void Update()
        {
            if (!HasReachedDestination())
            {
                Vector2 direction = _destination - Transformable.Position;
                float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Quaternion lerpRotation = Quaternion.Slerp(Transformable.Rotation, targetRotation, Time.deltaTime * _rotationSpeed);
                Vector2 smoothPosition = Vector2.SmoothDamp(Transformable.Position, Transformable.Position + direction * _velocity, ref _currentPosition, _deacceleration, _velocity);

                Transformable.Rotate(lerpRotation);
                Transformable.SetPosition(smoothPosition);

#if UNITY_EDITOR
                Debug.DrawLine(Transformable.Position, _destination, Color.yellow);
#endif
            }
            else _destination = GetRandomDestination();
        }
    }
    public class EnemyAttackState : EnemyBaseState
    {
        private readonly float _reloadTime;
        private float _timer;

        public EnemyAttackState(IStateMachine<EnemyController> stateMachine, EnemyModel model) : base(stateMachine)
        {
            _reloadTime = model.ReloadTime;
        }
        public override void Enter()
        {
            _timer = 0f;
        }
        public override void Exit()
        {

        }
        public override void Update()
        {
            if (_timer <= _reloadTime) _timer += Time.deltaTime;
            else
            {
                Context.Shoot();
                _timer = 0f;
            }
        }
    }
}