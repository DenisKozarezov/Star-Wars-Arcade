using UnityEngine;
using Zenject;

namespace Core
{
    public class EnemyBulletFactory : MonoBehaviour
    {
        private Bullet.Factory _factory;
        private float _timer;

        [Inject]
        private void Construct(Bullet.Factory factory)
        {
            _factory = factory;
        }
        private void Awake()
        {
            _timer = Time.realtimeSinceStartup;
        }
        private void Update()
        {
            if (Time.realtimeSinceStartup - _timer >= 2f)
            {
                _timer = Time.realtimeSinceStartup;
                Blast();
            }
        }
        private void Blast()
        {
            Bullet bullet = _factory.Create(Constants.BulletVelocity, BulletType.Enemy);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
        }
    }
}