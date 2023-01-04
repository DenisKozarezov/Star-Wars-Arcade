using Core.Input;
using Core.Weapons;

namespace Core.Player
{
    public class PlayerModel
    {
        public readonly float ReloadTime;
        public readonly float Velocity;
        public readonly float RotationSpeed;
        public readonly float Deacceleration;
        public readonly IInputSystem InputSystem;
        public IWeapon PrimaryWeapon { get; set; }
        public IWeapon SecondaryWeapon { get; set; }

        public PlayerModel(float reloadTime, float velocity, float rotationSpeed, float deacceleration, IInputSystem inputSystem)
        {
            ReloadTime = reloadTime;
            Velocity = velocity;
            RotationSpeed = rotationSpeed;
            Deacceleration = deacceleration;
            InputSystem = inputSystem;
        }
    }
}
