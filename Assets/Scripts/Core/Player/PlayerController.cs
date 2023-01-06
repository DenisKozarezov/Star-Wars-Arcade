using System;
using UnityEngine;
using Core.Weapons;
using Core.Units;

namespace Core.Player
{
    public class PlayerController : IUnit
    {
        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        private Camera _camera;

        public event Action<IUnit> WeaponHit;
        public event Action Died;

        public PlayerController(PlayerModel playerModel, PlayerView playerView)
        {
            _model = playerModel;
            _view = playerView;
            _camera = Camera.main;

            _model.Died += Died;
        }

        private void ProcessMovementInput(float deltaTime)
        {
            Vector2 direction = _model.InputSystem.Direction * _model.Velocity * deltaTime;
            _view.Translate(direction);
        }
        private void TrackCursor()
        {
            Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(_model.InputSystem.MouseScreenPosition);
            Vector3 lookDirection = mouseWorldPosition - (Vector3)_view.Position;
            Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.forward);
            _view.Rotate(rotation);
        }
        private void BindWeapon()
        {
            if (_model.PrimaryWeapon == null) return;

            _model.InputSystem.Fire += _model.PrimaryWeapon.Shoot;
            _model.PrimaryWeapon.Hit += WeaponHit.Invoke;
        }
        private void UnbindWeapon()
        {
            if (_model.PrimaryWeapon == null) return;

            _model.InputSystem.Fire -= _model.PrimaryWeapon.Shoot;
            _model.PrimaryWeapon.Hit -= WeaponHit.Invoke;
        }

        public void Enable()
        {
            _model.InputSystem.Enable();
            BindWeapon();

            _model.Died += Died;
        }
        public void Disable()
        {
            _model.InputSystem.Disable();
            UnbindWeapon();

            _model.Died -= Died;
        }
        public void SetPrimaryWeapon(IWeapon weapon)
        {
            if (weapon == null) throw new ArgumentNullException("Weapon is null!");

            UnbindWeapon();

            _model.PrimaryWeapon = weapon;

            BindWeapon();
        }
        public void Hit()
        {
            if (!_model.IsDead) _model.Hit();
        }
        public void Update(float deltaTime)
        {
            ProcessMovementInput(deltaTime);

            TrackCursor();
        }
    }
}
