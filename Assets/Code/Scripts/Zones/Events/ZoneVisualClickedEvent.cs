namespace Code.Scripts.Zones.Events
{
    public sealed class ZoneVisualClickedEvent
    {
        public ZoneVisual ZoneVisual { get; }
        
        public ZoneVisualClickedEvent(ZoneVisual zoneVisual)
        {
            ZoneVisual = zoneVisual;
        }
    }
}