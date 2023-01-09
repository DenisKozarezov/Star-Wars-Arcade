using System;
using Core.Weapons;

namespace Core.Units
{
    public class EnemyModel
    {
        private readonly int _maxHealth;

        public readonly float ReloadTime;
        public readonly float Velocity;
        public readonly float RotationSpeed;
        public readonly float Deacceleration;
        public readonly float AggressionRadius;
        public readonly float PatrolRadius;
        public int Health;
        public IWeapon PrimaryWeapon { get; set; }
        public bool IsDead => Health == 0;
        public event Action Died;

        public EnemyModel(
            float reloadTime, 
            float velocity, 
            float rotationSpeed, 
            float deacceleration, 
            float aggressionRadius,
            float patrolRadius,
            int maxHealth)
        {
            ReloadTime = reloadTime;
            Velocity = velocity;
            RotationSpeed = rotationSpeed;
            Deacceleration = deacceleration;
            AggressionRadius = aggressionRadius;
            PatrolRadius = patrolRadius;
            Health = maxHealth;
            _maxHealth = maxHealth;
        }

        public void Hit()
        {
            Health = Math.Max(Health - 1, 0);
            if (IsDead) Died?.Invoke();
        }
        public void Reset()
        {
            Health = _maxHealth;
        }
    }
}
