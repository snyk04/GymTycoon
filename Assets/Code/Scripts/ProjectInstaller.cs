using Code.Scripts.Resources;
using Code.Scripts.Save;
using Zenject;

namespace Code.Scripts
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<JsonGameSaveManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<AutoSaver>().AsSingle().NonLazy();
            Container.Bind<ResourcesHolder>().AsSingle().NonLazy();
        }
    }
}