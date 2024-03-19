using Code.Scripts.Audio;
using Zenject;

namespace Code.Scripts.DependencyInjection
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AudioManager>().AsSingle().NonLazy();
        }
    }
}