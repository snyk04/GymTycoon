using System.Collections.Generic;
using Code.Scripts.Save.Interfaces;

namespace Code.Scripts.Resources
{
    public sealed class ResourcesHolder
    {
        private readonly Dictionary<ResourceType, long> resourcesByTypes;

        private readonly IGameSaveManager gameSaveManager;
        
        public ResourcesHolder(IGameSaveManager gameSaveManager)
        {
            resourcesByTypes = gameSaveManager.SaveData.ResourcesByTypes;
            
            if (resourcesByTypes.Count == 0)
            {
                resourcesByTypes.Add(ResourceType.Money, 100);
                resourcesByTypes.Add(ResourceType.Diamonds, 100);
                gameSaveManager.Save();
            }
        }
    
        public long GetResource(ResourceType type)
        {
            return resourcesByTypes[type];
        }

        public void ChangeResource(ResourceType type, long delta)
        {
            if (resourcesByTypes[type] + delta < 0)
            {
                // TODO : Handle this situation
                return;
            }

            resourcesByTypes[type] += delta;
        }
    }
}