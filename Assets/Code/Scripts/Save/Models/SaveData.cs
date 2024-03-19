using System.Collections.Generic;
using Code.Scripts.Resources;

namespace Code.Scripts.Save.Models
{
    public sealed class SaveData
    {
        public Dictionary<ResourceType, long> ResourcesByTypes { get; } = new();
        public List<ZoneSaveData> ZoneSaveDataList { get; } = new();
    }
}