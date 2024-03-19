using System;
using Code.Scripts.Zones;
using Code.Scripts.Zones.Events;
using Zenject;

namespace Code.Scripts.Resources
{
    public sealed class ResourcesUpdater : IInitializable, IDisposable
    {
        private readonly EventBus eventBus;
        private readonly ResourcesHolder resourcesHolder;

        public ResourcesUpdater(EventBus eventBus, ResourcesHolder resourcesHolder)
        {
            this.eventBus = eventBus;
            this.resourcesHolder = resourcesHolder;
        }
        
        public void Initialize()
        {
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