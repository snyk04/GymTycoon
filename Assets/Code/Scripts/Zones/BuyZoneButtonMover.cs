using Code.Scripts.Zones.Events;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Zones
{
    public sealed class BuyZoneButtonMover : MonoBehaviour
    {
        [SerializeField] private BuyZoneButton buyZoneButtonPrefab;
        
        private EventBus eventBus;

        private BuyZoneButton buyZoneButton;
        
        [Inject]
        private void Construct(EventBus eventBus)
        {
            this.eventBus = eventBus;
            
            eventBus.Subscribe<ZoneVisualSpawnedEvent>(HandleZoneVisualSpawnedEvent);
        }

        private void HandleZoneVisualSpawnedEvent(ZoneVisualSpawnedEvent @event)
        {
            if (buyZoneButton == null)
            {
                buyZoneButton = Instantiate(buyZoneButtonPrefab, transform);
            }

            buyZoneButton.transform.position = @event.ZoneVisualPosition + @event.ZoneVisualOffset;
        }
    }
}