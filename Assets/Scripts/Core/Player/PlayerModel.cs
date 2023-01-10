using System;
using Core.Input;
using Core.Weapons;

namespace Core.Player
{
    public sealed class PlayerModel
    {
        public readonly float ReloadTime;
        public readonly float Velocity;
        public readonly float RotationSpeed;
        public readonly float Deacceleration;
        public readonly IInputSystem InputSystem;
        public IWeapon PrimaryWeapon { get; set; }
        public IWeapon SecondaryWeapon { get; set; }
        public bool IsDead => Health == 0;
        public int Health;

        public event Action<int> HealthChanged;
        public event Action Died;

        public PlayerModel(float reloadTime, float velocity, float rotationSpeed, float deacceleration, int maxHealth, IInputSystem inputSystem)
        {
            ReloadTime = reloadTime;
            Velocity = velocity;
            RotationSpeed = rotationSpeed;
            Deacceleration = deacceleration;
            InputSystem = inputSystem;
            Health = maxHealth;
        }

        public void Hit()
        {
            Health = Math.Max(Health - 1, 0);

            HealthChanged?.Invoke(Health);

            if (IsDead) Died?.Invoke();
        }
    }
}
