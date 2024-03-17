using UnityEngine;
using Zenject;

namespace Code.Scripts.Zones
{
    public sealed class ZoneSpawner : MonoBehaviour
    {
        [SerializeField] private ZoneSettings zoneSettings;
        [SerializeField] private Zone zonePrefab;
        [SerializeField] private Transform zoneParent;

        private EventBus eventBus;
        
        [Inject]
        private void Construct(EventBus eventBus)
        {
            this.eventBus = eventBus;
        }
        
        private void Start()
        {
            var zone = Instantiate(zonePrefab, zoneParent);
            zone.Initialize(eventBus, zoneSettings, 1);
        }
    }
}