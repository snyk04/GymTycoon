﻿using Code.Scripts.Resources;
using Code.Scripts.Save;
using Code.Scripts.Zones;
using UnityEngine;
using Zenject;

namespace Code.Scripts
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ZoneSettingsHolder zoneSettingsHolder;
        
        public override void InstallBindings()
        {
            Container.Bind<EventBus>().AsSingle().NonLazy();
            Container.BindInterfacesTo<JsonGameSaveManager>().AsSingle().NonLazy();
            Container.BindInterfacesTo<AutoSaver>().AsSingle().NonLazy();
            Container.Bind<ResourcesHolder>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ResourcesUpdater>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ZoneAmountOfUnitsSaver>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ZoneVisualPositionsHolder>().AsSingle().NonLazy();

            Container.Bind<ZoneSettingsHolder>().FromInstance(zoneSettingsHolder).AsSingle().NonLazy();
        }
    }
}