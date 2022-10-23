using Zenject;

namespace Core.Infrastructure.Signals
{
    public class SignalsInstaller : MonoInstaller<SignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<EnemyDestroyedSignal>();
        }
    }
}