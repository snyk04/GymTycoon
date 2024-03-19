using Code.Scripts.Zones.Events;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Zones.BuyZone
{
    public sealed class BuyZoneClicker : MonoBehaviour
    {
        private EventBus eventBus;

        [Inject]
        private void Construct(EventBus eventBus)
        {
            this.eventBus = eventBus;
        }
        
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }
            
            var ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (!Physics.Raycast(ray, out var hitInfo))
            {
                return;
            }
            
            if (!hitInfo.collider.TryGetComponent<BuyZoneButton>(out _))
            {
                return;
            }
                
            eventBus.RaiseEvent(new BuyZoneClickedEvent());
        }
    }
}