using UnityEngine;
using Zenject;

namespace Code.Scripts.Zones
{
    public sealed class ZoneSpawner : MonoBehaviour
    {
        [SerializeField] private ZoneSettings zoneSettings;
        [SerializeField] private Zone zonePrefab;
        [SerializeField] private Transform zoneParent;
        [SerializeField] private ZoneVisual zoneVisualPrefab;
        [SerializeField] private Transform zoneVisualParent;

        private EventBus eventBus;
        
        [Inject]
        private void Construct(EventBus eventBus)
        {
            this.eventBus = eventBus;
        }
        
        private void Start()
        {
            Spawn(zoneSettings);
        }

        private void Spawn(ZoneSettings zoneSettings)
        {
            var zone = Instantiate(zonePrefab, zoneParent);
            zone.Initialize(eventBus, zoneSettings, 1);

            var zoneVisual = Instantiate(zoneVisualPrefab, Vector3.up * 0.1f, Quaternion.identity, zoneVisualParent);
            zoneVisual.Initialize(eventBus, zoneSettings, zone);
        }
    }
}