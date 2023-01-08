using System;
using UnityEngine;
using Zenject;

namespace Core.Input
{
    public class StandaloneInputController : IInitializable, ITickable, ILateDisposable, IInputSystem
    {
        private readonly PlayerControls _playerControls;
        private Vector2 _mouseScreenPosition;
        private Vector2 _direction;
        public ref Vector2 Direction => ref _direction;
        public ref Vector2 MouseScreenPosition => ref _mouseScreenPosition;
        public bool Enabled => _playerControls.Player.enabled;
        public Action Fire { get; set; }
        
        public StandaloneInputController()
        {
            _playerControls = new PlayerControls();
            _playerControls.Player.Fire.started += _ => Fire?.Invoke();
        }
        void IInitializable.Initialize() => _playerControls.Enable();
        void ILateDisposable.LateDispose() => _playerControls.Disable();
        void ITickable.Tick()
        {
            if (!Enabled) return;

            _mouseScreenPosition = _playerControls.Player.MousePosition.ReadValue<Vector2>();
            _direction = _playerControls.Player.Movement.ReadValue<Vector2>();
        }
        public void Enable() => _playerControls.Enable();
        public void Disable()
        {
            _direction = Vector2.zero;
            _playerControls.Disable();
        }
    }
}