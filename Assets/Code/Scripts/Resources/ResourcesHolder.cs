using System.Collections.Generic;
using Code.Scripts.Resources;

public sealed class ResourcesHolder
{
    private readonly Dictionary<ResourceType, int> resourcesByTypes;

    public ResourcesHolder()
    {
        resourcesByTypes = new Dictionary<ResourceType, int>
        {
            { ResourceType.Money, 100 },
            { ResourceType.Diamonds, 100 }
        };
    }
    
    public int GetResource(ResourceType type)
    {
        return resourcesByTypes[type];
    }

    public void ChangeResource(ResourceType type, int delta)
    {
        if (resourcesByTypes[type] + delta < 0)
        {
            // TODO : Handle this situation somehow
            return;
        }

        resourcesByTypes[type] += delta;
    }
}