using System.Collections.Generic;
using Code.Scripts.Save.Interfaces;
using Code.Scripts.Save.Models;
using Code.Scripts.Zones.Models;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Zones
{
    public sealed class ZoneSpawner : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private ZoneSettingsByZoneType[] zoneSettingsByZoneTypeArray;
        [SerializeField] private Vector3 firstZoneVisualPosition;
        [SerializeField] private Vector3 offsetBetweenZones;

        [Header("Components")]
        [SerializeField] private Zone zonePrefab;
        [SerializeField] private Transform zoneParent;
        [SerializeField] private ZoneVisual zoneVisualPrefab;
        [SerializeField] private Transform zoneVisualParent;

        private Dictionary<ZoneType, ZoneSettings> zoneSettingsByZoneTypes;
        
        private IGameSaveManager gameSaveManager;
        private EventBus eventBus;
        
        [Inject]
        private void Construct(IGameSaveManager gameSaveManager, EventBus eventBus)
        {
            this.gameSaveManager = gameSaveManager;
            this.eventBus = eventBus;
        }

        private void Awake()
        {
            zoneSettingsByZoneTypes = new Dictionary<ZoneType, ZoneSettings>();
            foreach (var zoneSettingsByZoneType in zoneSettingsByZoneTypeArray)
            {
                zoneSettingsByZoneTypes.Add(zoneSettingsByZoneType.ZoneType, zoneSettingsByZoneType.ZoneSettings);
            }
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
                Spawn(zoneSettingsByZoneTypes[zoneSaveDataList[i].ZoneType], zoneSaveDataList[i].AmountOfUnits, i);
            }
        }

        private void Spawn(ZoneSettings zoneSettings, int amountOfUnits, int zoneIndex)
        {
            var zone = Instantiate(zonePrefab, zoneParent);
            zone.Initialize(zoneIndex, zoneSettings, amountOfUnits, eventBus);

            var position = firstZoneVisualPosition + zoneIndex * offsetBetweenZones;
            var zoneVisual = Instantiate(zoneVisualPrefab, position, Quaternion.identity, zoneVisualParent);
            zoneVisual.Initialize(eventBus, zoneSettings, zone);
        }
    }
}