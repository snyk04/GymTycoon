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

        [Header("Components")]
        [SerializeField] private Zone zonePrefab;
        [SerializeField] private Transform zoneParent;
        [SerializeField] private ZoneVisual zoneVisualPrefab;
        [SerializeField] private Transform zoneVisualParent;
        
        private IGameSaveManager gameSaveManager;
        private EventBus eventBus;
        private ZoneSettingsHolder zoneSettingsHolder;
        
        [Inject]
        private void Construct(IGameSaveManager gameSaveManager, EventBus eventBus, 
            ZoneSettingsHolder zoneSettingsHolder)
        {
            this.gameSaveManager = gameSaveManager;
            this.eventBus = eventBus;
            this.zoneSettingsHolder = zoneSettingsHolder;
            
            eventBus.Subscribe<ZoneBoughtEvent>(HandleZoneBoughtEvent);
        }

        private void HandleZoneBoughtEvent(ZoneBoughtEvent @event)
        {
            var newZoneSaveData = new ZoneSaveData(@event.ZoneType, AmountOfUnitsByDefault, @event.Position);
            gameSaveManager.SaveData.ZoneSaveDataList.Add(newZoneSaveData);
            gameSaveManager.Save();
            
            var zoneSettings = zoneSettingsHolder.ZoneSettingsByZoneTypes[@event.ZoneType];
            var zoneIndex = gameSaveManager.SaveData.ZoneSaveDataList.Count - 1;
            Spawn(zoneSettings, AmountOfUnitsByDefault, zoneIndex, @event.Position);
        }

        private void Start()
        {
            var zoneSaveDataList = gameSaveManager.SaveData.ZoneSaveDataList;
            if (zoneSaveDataList.Count == 0)
            {
                zoneSaveDataList.Add(new ZoneSaveData(ZoneType.Treadmill, 1, firstZoneVisualPosition));
            }

            for (var i = 0; i < zoneSaveDataList.Count; i++)
            {
                var zoneSettings = zoneSettingsHolder.ZoneSettingsByZoneTypes[zoneSaveDataList[i].ZoneType];
                var amountOfUnits = zoneSaveDataList[i].AmountOfUnits;
                var position = zoneSaveDataList[i].Position;
                Spawn(zoneSettings, amountOfUnits, i, position);
            }
        }

        private void Spawn(ZoneSettings zoneSettings, int amountOfUnits, int zoneIndex, Vector3 position)
        {
            var zone = Instantiate(zonePrefab, zoneParent);
            zone.Initialize(zoneIndex, zoneSettings, amountOfUnits, eventBus);

            var zoneVisual = Instantiate(zoneVisualPrefab, position, Quaternion.identity, zoneVisualParent);
            zoneVisual.Initialize(eventBus, zoneSettings, zone);
            eventBus.RaiseEvent(new ZoneVisualSpawnedEvent(position));
        }

        private void OnDestroy()
        {
            eventBus.Unsubscribe<ZoneBoughtEvent>(HandleZoneBoughtEvent);
        }
    }
}