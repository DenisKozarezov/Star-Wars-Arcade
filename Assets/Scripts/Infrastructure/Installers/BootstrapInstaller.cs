using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Core.Loading;

namespace Core.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGlobalDependencies();
        }

        private void BindGlobalDependencies()
        {
            Container.Bind<AudioListener>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<EventSystem>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<ILoadingScreenProvider>().To<LoadingScreenProvider>().AsSingle().NonLazy();
        }
    }
}