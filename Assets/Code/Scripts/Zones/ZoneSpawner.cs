using System;
using Code.Scripts.Save.Interfaces;
using Code.Scripts.Save.Models;
using Code.Scripts.Zones.Events;
using Code.Scripts.Zones.Models;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Zones
{
    public sealed class ZoneSpawner : MonoBehaviour
    {
        private const int AmountOfUnitsByDefault = 1;
        
        [Header("Settings")]
        [SerializeField] private Vector3 firstZoneVisualPosition;
        [SerializeField] private Vector3 offsetBetweenZones;

        [Header("Components")]
        [SerializeField] private Zone zonePrefab;
        [SerializeField] private Transform zoneParent;
        [SerializeField] private ZoneVisual zoneVisualPrefab;
        [SerializeField] private Transform zoneVisualParent;
        
        private IGameSaveManager gameSaveManager;
        private EventBus eventBus;
        private ZoneSettingsHolder zoneSettingsHolder;
        
        [Inject]
        private void Construct(IGameSaveManager gameSaveManager, EventBus eventBus, ZoneSettingsHolder zoneSettingsHolder)
        {
            this.gameSaveManager = gameSaveManager;
            this.eventBus = eventBus;
            this.zoneSettingsHolder = zoneSettingsHolder;
            
            eventBus.Subscribe<ZoneBoughtEvent>(HandleZoneBoughtEvent);
        }

        private void HandleZoneBoughtEvent(ZoneBoughtEvent obj)
        {
            var newZoneSaveData = new ZoneSaveData(obj.ZoneType, AmountOfUnitsByDefault);
            gameSaveManager.SaveData.ZoneSaveDataList.Add(newZoneSaveData);
            Spawn(zoneSettingsHolder.ZoneSettingsByZoneTypes[obj.ZoneType], AmountOfUnitsByDefault,  gameSaveManager.SaveData.ZoneSaveDataList.Count - 1);
        }

        private void Start()
        {
            var zoneSaveDataList = gameSaveManager.SaveData.ZoneSaveDataList;
            if (zoneSaveDataList.Count == 0)
            {
                zoneSaveDataList.Add(new ZoneSaveData(ZoneType.Treadmill, 1));
            }

            for (var i = 0; i < zoneSaveDataList.Count; i++)
            {
                Spawn(zoneSettingsHolder.ZoneSettingsByZoneTypes[zoneSaveDataList[i].ZoneType], zoneSaveDataList[i].AmountOfUnits, i);
            }
        }

        private void Spawn(ZoneSettings zoneSettings, int amountOfUnits, int zoneIndex)
        {
            var zone = Instantiate(zonePrefab, zoneParent);
            zone.Initialize(zoneIndex, zoneSettings, amountOfUnits, eventBus);

            var position = firstZoneVisualPosition + zoneIndex * offsetBetweenZones;
            var zoneVisual = Instantiate(zoneVisualPrefab, position, Quaternion.identity, zoneVisualParent);
            zoneVisual.Initialize(eventBus, zoneSettings, zone);
            eventBus.RaiseEvent(new ZoneVisualSpawnedEvent(position, offsetBetweenZones));
        }

        private void OnDestroy()
        {
            eventBus.Unsubscribe<ZoneBoughtEvent>(HandleZoneBoughtEvent);
        }
    }
}