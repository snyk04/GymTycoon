using System;
using Code.Scripts.Resources;
using UnityEngine;

namespace Code.Scripts.Zones
{
    [Serializable]
    public sealed class ZoneSettings
    {
        [field: SerializeField] public ResourceType ResourceType { get; private set; }
        
        [field: SerializeField] public int MaxUnits { get; private set; }
        [field: SerializeField] public int CycleLengthInMs { get; private set; }
        [field: SerializeField] public int ResourcePerCycle { get; private set; }
        [field: SerializeField] public int ResourcePerNewUnit { get; private set; }
        
        [field: SerializeField] public GameObject UnitPrefab { get; private set; }
    }
}