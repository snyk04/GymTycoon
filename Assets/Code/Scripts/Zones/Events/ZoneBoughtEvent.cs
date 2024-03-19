using Code.Scripts.Zones.Models;
using UnityEngine;

namespace Code.Scripts.Zones.Events
{
    public sealed class ZoneBoughtEvent
    {
        public ZoneType ZoneType { get; set; }
        public Vector3 Position { get; set; }

        public ZoneBoughtEvent(ZoneType zoneType, Vector3 position)
        {
            ZoneType = zoneType;
            Position = position;
        }
    }
}