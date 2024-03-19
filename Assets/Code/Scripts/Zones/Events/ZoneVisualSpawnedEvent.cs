using UnityEngine;

namespace Code.Scripts.Zones.Events
{
    public sealed class ZoneVisualSpawnedEvent
    {
        public Vector3 Position { get; }

        public ZoneVisualSpawnedEvent(Vector3 position)
        {
            Position = position;
        }
    }
}