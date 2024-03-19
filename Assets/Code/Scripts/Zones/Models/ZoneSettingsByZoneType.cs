using System;
using UnityEngine;

namespace Code.Scripts.Zones.Models
{
    [Serializable]
    public sealed class ZoneSettingsByZoneType
    {
        [field: SerializeField] public ZoneType ZoneType { get; private set; }
        [field: SerializeField] public ZoneSettings ZoneSettings { get; private set; }
    }
}