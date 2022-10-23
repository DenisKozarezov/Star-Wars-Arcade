using Zenject;
using Core.Input;

namespace Core
{
    public class PlayerBulletFactory : IInitializable, ILateDisposable
    {
        private readonly IInputSystem _inputSystem;
        private readonly PlayerView _player;
        private readonly Bullet.Factory _factory;

        public PlayerBulletFactory(IInputSystem inputSystem, PlayerView player, Bullet.Factory factory)
        {
            _inputSystem = inputSystem;
            _player = player;
            _factory = factory;
        }
        void IInitializable.Initialize()
        {
            _inputSystem.Fire += Blast;
        }
        void ILateDisposable.LateDispose()
        {
            _inputSystem.Fire -= Blast;
        }
        private void Blast()
        {
            Bullet bullet = _factory.Create(Constants.BulletVelocity, BulletType.Player);
            bullet.transform.position = _player.transform.position + _player.transform.up;
            bullet.transform.rotation = _player.transform.rotation;
        }
    }
}