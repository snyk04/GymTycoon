using Code.Scripts.Resources;
using UnityEngine;

namespace Code.Scripts.Zones.Events
{
    public struct ZoneProducedResourceEvent
    {
        public ResourceType ResourceType { get; }
        public int Amount { get; }
        public Vector3 ZonePosition { get; }

        public ZoneProducedResourceEvent(ResourceType resourceType, int amount, Vector3 zonePosition)
        {
            ResourceType = resourceType;
            Amount = amount;
            ZonePosition = zonePosition;
        }
    }
}