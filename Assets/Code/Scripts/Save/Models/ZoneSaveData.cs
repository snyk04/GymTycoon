using Code.Scripts.Utils;
using Code.Scripts.Zones.Models;
using Newtonsoft.Json;

namespace Code.Scripts.Save.Models
{
    public sealed class ZoneSaveData
    {
        public ZoneType ZoneType { get; }
        public int AmountOfUnits { get; set; }
        public SerializableVector3 Position { get; }
        
        [JsonConstructor]
        public ZoneSaveData(ZoneType zoneType, int amountOfUnits, SerializableVector3 position)
        {
            ZoneType = zoneType;
            AmountOfUnits = amountOfUnits;
            Position = position;
        }
    }
}