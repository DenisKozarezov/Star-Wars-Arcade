using System;
using UnityEngine;
using Zenject;

namespace Core.Audio
{
    [Serializable]
    public struct AudioSound
    {
        public AudioClip Clip;
        [Range(0f, 1f)]
        public float Volume;
    }

    [Serializable]
    public class AudioSettings
    {
        public byte SoundsPoolCapacity;

        [Space, SerializeField]
        public GameSounds GameSounds;
        [SerializeField]
        public UISounds UISounds;
        [SerializeField]
        public PlayerSounds PlayerSounds;
    }

    [Serializable]
    public class GameSounds
    {
        public AudioSound GameBackground;
        public AudioSound GameLevelStart;
        public AudioSound GameOver;
    }

    [Serializable]
    public class UISounds
    {
        public AudioSound UIClick;
    }

    [Serializable]
    public class PlayerSounds
    {
        public AudioSound PlayerOnHit;
    }
}

namespace Core.Models
{ 
    [CreateAssetMenu(menuName = "Configuration/Settings/Audio Settings")]
    public class AudioSettingsInstaller : ScriptableObjectInstaller<AudioSettingsInstaller>
    {
        [SerializeField]
        private Audio.AudioSettings _audioSettings;

        public override void InstallBindings()
        {
            Container.Bind<Audio.AudioSettings>().FromInstance(_audioSettings).IfNotBound();
        }
    }
}