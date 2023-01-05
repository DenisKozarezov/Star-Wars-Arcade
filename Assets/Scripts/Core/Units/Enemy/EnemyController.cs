using System;
using Core.Weapons;

namespace Core.Units
{
    public class EnemyController : IUnit
    {
        private readonly EnemyModel _model;
        private readonly EnemyView _view;
        private readonly EnemyStateMachine _stateMachine;

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
        public void Update() => _stateMachine.Update();
        public void Hit()
        {
            _model.Hit();
            Dispose();
        }
        public void Dispose()
        {
            _view.Dispose();
            Disposed?.Invoke(this);
        }
        public void Reset() => _model.Reset();
    }
}
