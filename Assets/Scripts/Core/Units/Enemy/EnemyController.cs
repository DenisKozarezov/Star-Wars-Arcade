using System;
using UnityEngine;
using Zenject;
using Core.Weapons;

namespace Core.Units
{
    public class EnemyController : IUnit, IPoolable<Vector2>, IDisposable
    {
        private readonly EnemyModel _model;
        private readonly EnemyView _view;
        private readonly EnemyStateMachine _stateMachine;

        public ITransformable Transformable => _view;
        public bool IsDead => _model.IsDead;
        public event Action<IUnit> WeaponHit;
        public event Action<EnemyController> Disposed;

        public EnemyController(EnemyModel enemyModel, EnemyView enemyView)
        {
            _model = enemyModel;
            _view = enemyView;
            _stateMachine = new EnemyStateMachine(this, enemyModel);
        }

        private void OnWeaponHit(IUnit target)
        {
            WeaponHit?.Invoke(target);
        }

        public void SetPrimaryWeapon(IWeapon weapon)
        {
            if (weapon == null) throw new ArgumentNullException("Weapon is null!");

            if (_model.PrimaryWeapon != null)
                _model.PrimaryWeapon.Hit -= OnWeaponHit;

            _model.PrimaryWeapon = weapon;
            _model.PrimaryWeapon.Hit += OnWeaponHit;
        }
        public void Shoot() => _model.PrimaryWeapon.Shoot();
        public void Update()
        {
            if (_model.IsDead) return;

            _stateMachine.Update();
        }
        public void Hit()
        {
            if (!_model.IsDead)
            {
                _model.Hit();
                Dispose();
            }
        }
        public void Dispose()
        {
            Disposed?.Invoke(this);
        }

        void IPoolable<Vector2>.OnDespawned() => _view.SetActive(false);
        void IPoolable<Vector2>.OnSpawned(Vector2 position)
        {
            _model.Reset();
            _view.SetPosition(position);
            _view.SetActive(true);
        }
    }
}
