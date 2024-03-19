using Code.Scripts.Zones.Events;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Zones
{
    public sealed class ZoneClicker : MonoBehaviour
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
            
            if (!hitInfo.collider.TryGetComponent<ZoneVisual>(out var zoneVisual))
            {
                return;
            }
                
            eventBus.RaiseEvent(new ZoneVisualClickedEvent(zoneVisual));
        }
    }
}