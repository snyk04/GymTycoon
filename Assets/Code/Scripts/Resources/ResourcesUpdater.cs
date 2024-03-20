using System;
using Code.Scripts.Zones.Events;

namespace Code.Scripts.Resources
{
    public sealed class ResourcesUpdater : IDisposable
    {
        private readonly EventBus eventBus;
        private readonly ResourcesHolder resourcesHolder;

        public ResourcesUpdater(EventBus eventBus, ResourcesHolder resourcesHolder)
        {
            this.eventBus = eventBus;
            this.resourcesHolder = resourcesHolder;
            
            eventBus.Subscribe<ZoneProducedResourceEvent>(HandleZoneProducedResource);
        }
        
        public void Dispose()
        {
            eventBus.Unsubscribe<ZoneProducedResourceEvent>(HandleZoneProducedResource);
        }

        private void HandleZoneProducedResource(ZoneProducedResourceEvent @event)
        {
            resourcesHolder.ChangeResource(@event.ResourceType, @event.Amount);
        }
    }
}