using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Core.Loading;
using Core.Audio;

namespace Core.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        private byte _soundsPoolCapacity;

        [Inject]
        private void Construct(Audio.AudioSettings audioSettings)
        {
            _soundsPoolCapacity = audioSettings.SoundsPoolCapacity;
        }

        public override void InstallBindings()
        {
            BindGlobalDependencies();
        }

        private void BindGlobalDependencies()
        {
            Container.Bind<AudioListener>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<SoundManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<ILoadingScreenProvider>().To<LoadingScreenProvider>().AsSingle().NonLazy();
            Container.Bind<ICoroutineRunner>().To<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.BindFactory<AudioClip, float, bool, DisposableAudioClip, DisposableAudioClip.Factory>()
               .FromMonoPoolableMemoryPool(x => x
               .WithInitialSize(_soundsPoolCapacity)
               .FromNewComponentOnNewGameObject()
               .UnderTransformGroup("Sounds"));
        }
    }
}