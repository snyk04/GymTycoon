using Code.Scripts.Resources;

namespace Code.Scripts.Zones
{
    public struct ZoneProducedResourceEvent
    {
        public ResourceType ResourceType { get; }
        public int Amount { get; }

        public ZoneProducedResourceEvent(ResourceType resourceType, int amount)
        {
            ResourceType = resourceType;
            Amount = amount;
        }
    }
}