using Code.Scripts.Zones.Models;

namespace Code.Scripts.Save.Models
{
    public sealed class ZoneSaveData
    {
        public ZoneType ZoneType { get; }
        public int AmountOfUnits { get; set; }

        public ZoneSaveData(ZoneType zoneType, int amountOfUnits)
        {
            ZoneType = zoneType;
            AmountOfUnits = amountOfUnits;
        }
    }
}