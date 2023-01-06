using Core.Weapons;

namespace Core.Units
{
    public class EnemyModel
    {
        public readonly float ReloadTime;
        public readonly float Velocity;
        public readonly float RotationSpeed;
        public readonly float Deacceleration;
        public readonly float AggressionRadius;
        public readonly float PatrolRadius;
        public byte Health;
        public IWeapon PrimaryWeapon { get; set; }
        public IWeapon SecondaryWeapon { get; set; }
        public bool IsDead => Health == 0;

        public EnemyModel(
            float reloadTime, 
            float velocity, 
            float rotationSpeed, 
            float deacceleration, 
            float aggressionRadius, 
            float patrolRadius)
        {
            ReloadTime = reloadTime;
            Velocity = velocity;
            RotationSpeed = rotationSpeed;
            Deacceleration = deacceleration;
            AggressionRadius = aggressionRadius;
            PatrolRadius = patrolRadius;
        }

        public void Hit() => Health = 0;
        public void Reset()
        {
            Health = 1;
        }
    }
}
