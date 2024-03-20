using Code.Scripts.Audio;
using UnityEngine;
using Zenject;

namespace Code.Scripts.DependencyInjection
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private ButtonAudioSettings buttonAudioSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<AudioManager>().AsSingle().NonLazy();
            Container.Bind<ButtonAudioSettings>().FromInstance(buttonAudioSettings).AsSingle().NonLazy();
        }
    }
}