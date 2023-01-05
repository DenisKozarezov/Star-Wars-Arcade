using Core.Input;
using Core.Units;
using Core.Weapons;
using System;

namespace Core.Player
{
    public class PlayerModel
    {
        private int Health;
        public readonly float ReloadTime;
        public readonly float Velocity;
        public readonly float RotationSpeed;
        public readonly float Deacceleration;
        public readonly IInputSystem InputSystem;
        public IWeapon PrimaryWeapon { get; set; }
        public IWeapon SecondaryWeapon { get; set; }

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
            if (Health == 0) Died?.Invoke();
        }
    }
}
