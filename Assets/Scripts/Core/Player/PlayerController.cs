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

        public ITransformable Transformable => _view;
        public event Action<IUnit> WeaponHit;
        public event Action Died;

        public PlayerController(PlayerModel playerModel, PlayerView playerView)
        {
            _model = playerModel;
            _view = playerView;
            _camera = Camera.main;
        }

        private void OnWeaponHit(IUnit target)
        {
            WeaponHit?.Invoke(target);
        }
        private void OnDied()
        {
            Died?.Invoke();
        }

        private void ProcessMovementInput()
        {
            _view.Translate(_model.InputSystem.Direction, _model.Velocity, _model.Deacceleration);
        }
        private void TrackCursor()
        {
            Vector3 position = _camera.WorldToScreenPoint(_view.Position);
            Vector3 direction = (Vector3)_model.InputSystem.MouseScreenPosition - position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            _view.Rotate(Quaternion.AngleAxis(angle, Vector3.forward));
        }
        private void BindWeapon()
        {
            if (_model.PrimaryWeapon == null) return;

            _model.InputSystem.Fire += _model.PrimaryWeapon.Shoot;
            _model.PrimaryWeapon.Hit += OnWeaponHit;
        }
        private void UnbindWeapon()
        {
            if (_model.PrimaryWeapon == null) return;

            _model.InputSystem.Fire -= _model.PrimaryWeapon.Shoot;
            _model.PrimaryWeapon.Hit -= OnWeaponHit;
        }

        public void Enable()
        {
            _model.InputSystem.Enable();
            _model.Died += OnDied;
        }
        public void Disable()
        {
            _model.InputSystem.Disable();
            _model.Died -= OnDied;
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
        public void Update()
        {
            if (_model.InputSystem.Enabled)
            {
                ProcessMovementInput();

                TrackCursor();
            }
        }
    }
}
