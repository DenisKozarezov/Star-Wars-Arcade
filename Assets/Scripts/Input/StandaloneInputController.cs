using System;
using UnityEngine;
using Zenject;

namespace Core.Input
{
    public class StandaloneInputController : IInitializable, ITickable, ILateDisposable, IInputSystem
    {
        private readonly PlayerControls _playerControls;
        private Vector2 _mousePosition;
        private Vector2 _direction;
        public ref Vector2 Direction => ref _direction;
        public ref Vector2 MousePosition => ref _mousePosition;
        public bool Enabled => _playerControls.Player.enabled;
        public Action Fire { get; set; }
        
        public StandaloneInputController()
        {
            _playerControls = new PlayerControls();
            _playerControls.Player.Fire.started += _ => Fire?.Invoke();
        }
        void IInitializable.Initialize()
        {
            _playerControls.Enable();
        }
        void ILateDisposable.LateDispose()
        {
            _playerControls.Disable();
        }
        void ITickable.Tick()
        {
            if (!Enabled) return;

            _mousePosition = _playerControls.Player.MousePosition.ReadValue<Vector2>();
            _direction = _playerControls.Player.Movement.ReadValue<Vector2>();
        }
    }
}