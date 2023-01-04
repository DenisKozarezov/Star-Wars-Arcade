using System;
using UnityEngine;
using Zenject;
using Core.Weapons;
using Core.Units;

namespace Core.Player
{
    public class PlayerController : ITickable, IUnit
    {
        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        public ITransformable Transformable => _view;
        public event Action<IUnit> WeaponHit;

        public PlayerController(PlayerModel playerModel, PlayerView playerView)
        {
            _model = playerModel;
            _view = playerView;
        }

        private void OnFire() => _model.PrimaryWeapon.Shoot();

        private void ProcessMovementInput()
        {
            Vector2 direction = _model.InputSystem.Direction * _model.Velocity * Time.deltaTime;
            Transformable.Translate(direction);
        }
        private void TrackCursor()
        {
            Vector3 mouseWorldPosition = _model.InputSystem.MousePosition;
            Vector3 lookDirection = mouseWorldPosition - _view.transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            Transformable.Rotate(Quaternion.Euler(0f, 0f, angle));
        }
        private void BindWeapon()
        {
            if (_model.PrimaryWeapon == null) throw new ArgumentNullException("Weapon is null!");

            _model.InputSystem.Fire += OnFire;
            _model.PrimaryWeapon.Hit += WeaponHit.Invoke;
        }
        private void UnbindWeapon()
        {
            if (_model.PrimaryWeapon == null) throw new ArgumentNullException("Weapon is null!");

            _model.InputSystem.Fire -= OnFire;
            _model.PrimaryWeapon.Hit -= WeaponHit.Invoke;
        }

        public void Enable()
        {
            _model.InputSystem.Enable();
            BindWeapon();
        }
        public void Disable()
        {
            _model.InputSystem.Disable();
            UnbindWeapon();
        }
        public void SetPrimaryWeapon(IWeapon weapon)
        {
            if (weapon == null) throw new ArgumentNullException("Weapon is null!");

            UnbindWeapon();

            _model.PrimaryWeapon = weapon;

            BindWeapon();
        }

        void ITickable.Tick()
        {
            ProcessMovementInput();

            TrackCursor();
        }
    }
}
