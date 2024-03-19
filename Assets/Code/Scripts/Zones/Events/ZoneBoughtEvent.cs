using Code.Scripts.Zones.Models;

namespace Code.Scripts.Zones.Events
{
    public sealed class ZoneBoughtEvent
    {
        public ZoneType ZoneType { get; set; }

        public ZoneBoughtEvent(ZoneType zoneType)
        {
            ZoneType = zoneType;
        }
    }
}