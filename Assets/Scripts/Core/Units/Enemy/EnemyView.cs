using System;
using UnityEngine;
using Zenject;

namespace Core.Units
{
    public class EnemyView : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable, ITransformable
    {
        [field: SerializeField] public Transform FirePoint { get; private set; }

        private IMemoryPool _pool;
        public Vector2 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        public void Dispose()
        {
            _pool?.Despawn(this);
        }
        public void Rotate(Quaternion rotation) => transform.rotation = rotation;
        public void Translate(Vector2 direction) => transform.Translate(direction, Space.World);
        public void SetPosition(Vector2 position) => transform.position = position;

        void IPoolable<IMemoryPool>.OnDespawned()
        {
            _pool = null;
        }
        void IPoolable<IMemoryPool>.OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }

        public class Factory : PlaceholderFactory<EnemyView> { }
    }
}