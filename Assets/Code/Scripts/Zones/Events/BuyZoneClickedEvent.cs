using UnityEngine;

namespace Code.Scripts.Zones.Events
{
    public sealed class BuyZoneClickedEvent
    {
        public Vector3 Position { get; }

        public BuyZoneClickedEvent(Vector3 position)
        {
            Position = position;
        }
    }
}