using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Core.Units;

namespace Core.Weapons
{
    public enum BulletType : byte
    {
        Player = 0,
        Enemy = 1
    }

    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour, IPoolable<Vector2, Quaternion, float, float, BulletType, IMemoryPool>, IDisposable
    {
        private Collider2D _collider;
        private IMemoryPool _pool;
        private float _bulletForce;
        private Coroutine _lifetimeCoroutine;

        public event Action LifetimeElapsed;
        public event Action<Bullet, ITransformable> Hit;
        public event Action<Bullet> Disposed;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }
        private void Update()
        {
            transform.position += transform.right * _bulletForce * Time.deltaTime;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out ITransformable target))
            {
                Hit?.Invoke(this, target);
            }
        }
        private void Enable(bool isEnabled)
        {
            _collider.enabled = isEnabled;
            _collider.attachedRigidbody.simulated = isEnabled;
            _collider.attachedRigidbody.constraints = isEnabled ? RigidbodyConstraints2D.FreezeRotation : RigidbodyConstraints2D.FreezeAll;
        }
        public void Dispose()
        {
            if (_lifetimeCoroutine != null)
            {
                StopCoroutine(_lifetimeCoroutine);
                _lifetimeCoroutine = null;
            }
            Disposed?.Invoke(this);
            _pool?.Despawn(this);
        }

        void IPoolable<Vector2, Quaternion, float, float, BulletType, IMemoryPool>.OnDespawned()
        {
            _pool = null;
            Enable(false);
        }
        void IPoolable<Vector2, Quaternion, float, float, BulletType, IMemoryPool>.OnSpawned(Vector2 position, Quaternion rotation, float force, float lifetime, BulletType bulletType, IMemoryPool pool)
        {
            _pool = pool;
            transform.position = position;
            transform.rotation = rotation;
            _bulletForce = force;
            Enable(true);
            _lifetimeCoroutine = StartCoroutine(LifetimeCoroutine(lifetime));
        }
        private IEnumerator LifetimeCoroutine(float lifetime)
        {
            yield return new WaitForSeconds(lifetime);
            LifetimeElapsed?.Invoke();
        }

        public class Factory : PlaceholderFactory<Vector2, Quaternion, float, float, BulletType, Bullet> { }
    }
}