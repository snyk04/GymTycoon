using Code.Scripts.Zones.Events;
using Code.Scripts.Zones.Models;
using UnityEngine;

namespace Code.Scripts.Zones
{
    public sealed class ZoneVisual : MonoBehaviour
    {
        [SerializeField] private Transform unitParent;
        [SerializeField] private Vector3 firstUnitPosition;
        [SerializeField] private int amountOfRows;
        [SerializeField] private int amountOfColumns;
        [SerializeField] private float rowOffset;
        [SerializeField] private float columnOffset;
        
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
                var unit = Instantiate(unitPrefab, unitParent);
                var offset = i % amountOfColumns * columnOffset * Vector3.right + i / amountOfColumns * rowOffset * Vector3.back;
                unit.transform.localPosition = firstUnitPosition + offset;
            }
        }

        private void OnDestroy()
        {
            eventBus.Unsubscribe<ZoneAmountOfUnitsIncreasedEvent>(HandleZoneAmountOfUnitsIncreasedEvent);
        }
    }
}