using Core.Weapons;

namespace Core.Units
{
    public class EnemyModel
    {
        public readonly float ReloadTime;
        public readonly float Velocity;
        public readonly float RotationSpeed;
        public readonly float Deacceleration;
        public byte Health;
        public IWeapon PrimaryWeapon { get; set; }
        public IWeapon SecondaryWeapon { get; set; }

        public EnemyModel(float reloadTime, float velocity, float rotationSpeed, float deacceleration)
        {
            ReloadTime = reloadTime;
            Velocity = velocity;
            RotationSpeed = rotationSpeed;
            Deacceleration = deacceleration;
        }

        public void Hit() => Health = 0;
        public void Reset()
        {
            Health = 1;
        }
    }
}
