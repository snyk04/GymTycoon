using System;
using System.Collections.Generic;
using Code.Scripts.Zones.Events;
using UnityEngine;

namespace Code.Scripts.Zones
{
    public sealed class ZoneVisualPositionsHolder : IDisposable
    {
        private readonly List<Vector3> zoneVisualPositions = new();
        public IEnumerable<Vector3> ZoneVisualPositions => zoneVisualPositions;
        
        private readonly EventBus eventBus;

        public ZoneVisualPositionsHolder(EventBus eventBus)
        {
            this.eventBus = eventBus;
            
            eventBus.Subscribe<ZoneVisualSpawnedEvent>(HandleZoneVisualSpawnedEvent);
        }

        private void HandleZoneVisualSpawnedEvent(ZoneVisualSpawnedEvent @event)
        {
            zoneVisualPositions.Add(@event.Position);
        }

        public void Dispose()
        {
            eventBus.Unsubscribe<ZoneVisualSpawnedEvent>(HandleZoneVisualSpawnedEvent);
        }
    }
}