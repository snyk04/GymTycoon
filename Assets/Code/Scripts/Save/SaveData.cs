using System.Collections.Generic;
using Code.Scripts.Resources;

namespace Code.Scripts.Save
{
    public sealed class SaveData
    {
        public Dictionary<ResourceType, int> ResourcesByTypes { get; set; } = new();
    }
}