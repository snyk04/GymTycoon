namespace Code.Scripts.Zones.Events
{
    public sealed class ZoneAmountOfUnitsIncreasedEvent
    {
        public Zone Zone { get; }
        
        public ZoneAmountOfUnitsIncreasedEvent(Zone zone)
        {
            Zone = zone;
        }
    }
}