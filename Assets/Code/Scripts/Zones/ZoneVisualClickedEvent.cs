namespace Code.Scripts.Zones
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