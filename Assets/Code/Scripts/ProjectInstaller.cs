using Code.Scripts.Resources;
using Code.Scripts.Save;
using Code.Scripts.Zones;
using Zenject;

namespace Code.Scripts
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventBus>().AsSingle().NonLazy();
            Container.BindInterfacesTo<JsonGameSaveManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<AutoSaver>().AsSingle().NonLazy();
            Container.Bind<ResourcesHolder>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ResourcesUpdater>().AsSingle().NonLazy();
        }
    }
}