using System;
using UnityEngine;
using Zenject;
using Core.Weapons;

namespace Core.Units
{
    public class EnemyController : IUnit, IEnemy, IPoolable<Vector2>, IDisposable
    {
        private readonly EnemyModel _model;
        private readonly EnemyView _view;
        private readonly EnemyStateMachine _stateMachine;

        public IUnit Target { get; private set; }
        public bool IsTaunted => Target != null;
        public ITransformable Transformable => _view;
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
        public void Hit()
        {
            if (!_model.IsDead) _model.Hit();
        }
        public void Taunt(IUnit unit)
        {
            Target = unit;
            _stateMachine.SwitchState<EnemyAttackState>();
        }
        public void Dispose()
        {
            Disposed?.Invoke(this);
        }
        public void Update()
        {
            if (_model.IsDead) return;

            _stateMachine.CurrentState.Update();
        }

        void IPoolable<Vector2>.OnDespawned()
        {
            _model.Died -= Dispose;
            _view.SetActive(false);
            Target = null;
        }
        void IPoolable<Vector2>.OnSpawned(Vector2 position)
        {
            _model.Reset();
            _model.Died += Dispose;
            _view.SetPosition(position);
            _view.SetActive(true);
            _stateMachine.SwitchState<EnemyPatrolState>();
        }
    }
}
