using Zenject;
using Core.Units;

namespace Core.Infrastructure
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemyStateMachine>().AsSingle();
        }
    }
}