using UnityEngine;

namespace Core.Units
{   
    public class EnemyPatrolState : EnemyBaseState
    {
        private readonly float _velocity;
        private readonly float _rotationSpeed;
        private readonly float _deacceleration;
        private readonly float _patrolRadius;
        private Vector2 _initPosition;
        private Vector2 _destination;

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
        private bool HasReachedDestination(out Vector2 direction)
        {
            direction = _destination - Transformable.Position;
            return (_destination - Transformable.Position).sqrMagnitude <= 1f;
        }
        private void LookAtTarget(Vector2 direction)
        {
            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Quaternion lerpRotation = Quaternion.Slerp(Transformable.Rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            Transformable.Rotate(lerpRotation);
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
            if (!HasReachedDestination(out Vector2 direction))
            {
                LookAtTarget(direction);
                Transformable.Translate(direction.normalized, _velocity, _deacceleration);

#if UNITY_EDITOR
                Debug.DrawLine(Transformable.Position, _destination, Color.yellow);
#endif
            }
            else _destination = GetRandomDestination();
        }
    }    
}