using UnityEngine;

namespace Code.Scripts.Zones.Events
{
    public sealed class ZoneVisualSpawnedEvent
    {
        public Vector3 ZoneVisualPosition { get; }
        public Vector3 ZoneVisualOffset { get; }

        public ZoneVisualSpawnedEvent(Vector3 zoneVisualPosition, Vector3 zoneVisualOffset)
        {
            ZoneVisualPosition = zoneVisualPosition;
            ZoneVisualOffset = zoneVisualOffset;
        }
    }
}