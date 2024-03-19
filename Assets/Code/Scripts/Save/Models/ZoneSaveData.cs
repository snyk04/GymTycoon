using Code.Scripts.Utils;
using Code.Scripts.Zones.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Scripts.Save.Models
{
    public sealed class ZoneSaveData
    {
        public ZoneType ZoneType { get; }
        public int AmountOfUnits { get; set; }
        [JsonIgnore] public Vector3 Position { get; set; }
        public SerializableVector3 SerializablePosition { get; set; }

        public ZoneSaveData(ZoneType zoneType, int amountOfUnits, Vector3 position)
        {
            ZoneType = zoneType;
            AmountOfUnits = amountOfUnits;
            Position = position;
            SerializablePosition = position.ToSerializable();
        }
        
        [JsonConstructor]
        public ZoneSaveData(ZoneType zoneType, int amountOfUnits, SerializableVector3 serializablePosition)
        {
            ZoneType = zoneType;
            AmountOfUnits = amountOfUnits;
            Position = serializablePosition.ToVector3();
            SerializablePosition = serializablePosition;
        }
    }
}