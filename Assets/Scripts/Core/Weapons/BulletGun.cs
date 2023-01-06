using System;
using UnityEngine;
using Core.Units;

namespace Core.Weapons
{
    public class BulletGun : IWeapon
    {
        private readonly BulletGunModel _model;
        private readonly Bullet.Factory _bulletFactory;
        private readonly Explosion.Factory _explosionFactory;

        public event Action<IUnit> Hit;

        public BulletGun(BulletGunModel model, Bullet.Factory bulletFactory, Explosion.Factory explosionFactory)
        {
            _model = model;
            _bulletFactory = bulletFactory;
            _explosionFactory = explosionFactory;
        }

        public void Shoot()
        {
            if (!_model.Cooldown.IsOver) return;

            Bullet bullet = _bulletFactory.Create
            (
                _model.FirePoint.position,
                _model.FirePoint.rotation,
                _model.BulletGunConfig.BulletForce,
                _model.BulletGunConfig.BulletLifetime,
                _model.BulletType
            );

            bullet.LifetimeElapsed += bullet.Dispose;
            bullet.Hit += OnBulletHit;
            bullet.Disposed += OnDisposed;

            _model.Cooldown.Run(_model.ReloadTime);
        }

        private void CreateExplosion(Vector2 position)
        {
            Explosion explosion = _explosionFactory.Create();
            explosion.transform.position = position;
            explosion.Invoke(nameof(explosion.Dispose), 1.5f);
        }

        private void OnDisposed(Bullet bullet)
        {
            bullet.LifetimeElapsed -= bullet.Dispose;
            bullet.Hit -= OnBulletHit;
            bullet.Disposed -= OnDisposed;
        }
        private void OnBulletHit(Bullet bullet, IUnit unit)
        {
            CreateExplosion(bullet.transform.position);
            bullet.Dispose();
            Hit?.Invoke(unit);
        }
    }
}
