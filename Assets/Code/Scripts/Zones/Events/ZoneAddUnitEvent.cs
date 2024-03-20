namespace Code.Scripts.Zones.Events
{
    public sealed class ZoneAddUnitEvent
    {
        public Zone Zone { get; }
        
        public ZoneAddUnitEvent(Zone zone)
        {
            Zone = zone;
        }
    }
}