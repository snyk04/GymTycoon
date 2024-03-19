using UnityEngine;

namespace Code.Scripts.Zones.Events
{
    public sealed class BuyZoneClickedEvent
    {
        public Vector3 Position { get; set; }

        public BuyZoneClickedEvent(Vector3 position)
        {
            Position = position;
        }
    }
}