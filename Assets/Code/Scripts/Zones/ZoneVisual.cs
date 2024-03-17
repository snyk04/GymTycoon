using UnityEngine;

namespace Code.Scripts.Zones
{
    public sealed class ZoneVisual : MonoBehaviour
    {
        [SerializeField] private Transform unitParent;

        private EventBus eventBus;
        private ZoneSettings zoneSettings;
        public Zone Zone { get; private set; }
        
        public void Initialize(EventBus eventBus, ZoneSettings zoneSettings, Zone zone)
        {
            this.eventBus = eventBus;
            this.zoneSettings = zoneSettings;
            this.Zone = zone;

            eventBus.Subscribe<ZoneAmountOfUnitsIncreasedEvent>(HandleZoneAmountOfUnitsIncreasedEvent);
            
            SpawnUnits(zone.AmountOfUnits, this.zoneSettings.UnitPrefab);
        }

        private void HandleZoneAmountOfUnitsIncreasedEvent(ZoneAmountOfUnitsIncreasedEvent @event)
        {
            if (@event.Zone == Zone)
            {
                SpawnUnits(Zone.AmountOfUnits, zoneSettings.UnitPrefab);
            }
        }

        private void SpawnUnits(int amountOfUnits, GameObject unitPrefab)
        {
            for (var i = 0; i < unitParent.childCount; i++)
            {
                Destroy(unitParent.GetChild(i).gameObject);
            }

            for (var i = 0; i < amountOfUnits; i++)
            {
                Instantiate(unitPrefab, unitParent);
            }
        }

        private void OnDestroy()
        {
            eventBus.Unsubscribe<ZoneAmountOfUnitsIncreasedEvent>(HandleZoneAmountOfUnitsIncreasedEvent);
        }
    }
}