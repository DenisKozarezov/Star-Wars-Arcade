using UnityEngine;
using Zenject;
using Core.Input;
using Core.Models;

namespace Core
{
    public class PlayerView : MonoBehaviour, IUnit
    {
        private IInputSystem _inputSystem;
        private PlayerSettings _settings;
        private Camera _cachedCamera;
        private bool _destroyed;
        private Vector3 _currentPosition;

        [Inject]
        private void Construct(IInputSystem inputSystem, PlayerSettings settings)
        {
            _inputSystem = inputSystem;
            _settings = settings;
        }
        private void Awake()
        {
            _cachedCamera = Camera.main;
        }
        private void Update()
        {
            if (_destroyed) return;

            // Rotation
            Vector3 direction = _cachedCamera.ScreenToWorldPoint(_inputSystem.MousePosition) - transform.position;
            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _settings.PlayerModel.RotationSpeed);

            // Movement
            float velocity = _settings.PlayerModel.Velocity;
            float deacceleration = _settings.PlayerModel.Deacceleration;
            transform.position = Vector3.SmoothDamp(transform.position, transform.position + (Vector3)_inputSystem.Direction * velocity, ref _currentPosition, deacceleration, velocity);
        }

        public void Kill()
        {
            _destroyed = true;
        }
    }
}