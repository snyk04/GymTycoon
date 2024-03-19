using System.Collections.Generic;
using Code.Scripts.Zones.Models;
using UnityEngine;

namespace Code.Scripts.Zones
{
    public sealed class ZoneSettingsHolder : MonoBehaviour
    {
        [SerializeField] private ZoneSettingsByZoneType[] zoneSettingsByZoneTypeArray;

        private Dictionary<ZoneType, ZoneSettings> zoneSettingsByZoneTypes;
        public IReadOnlyDictionary<ZoneType, ZoneSettings> ZoneSettingsByZoneTypes => zoneSettingsByZoneTypes;

        private void Awake()
        {
            zoneSettingsByZoneTypes = new Dictionary<ZoneType, ZoneSettings>();
            foreach (var zoneSettingsByZoneType in zoneSettingsByZoneTypeArray)
            {
                zoneSettingsByZoneTypes.Add(zoneSettingsByZoneType.ZoneType, zoneSettingsByZoneType.ZoneSettings);
            }
        }
    }
}