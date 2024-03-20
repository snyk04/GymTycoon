using Code.Scripts.Zones.Events;
using Code.Scripts.Zones.Models;
using UnityEngine;

namespace Code.Scripts.Zones
{
    public sealed class ZoneVisual : MonoBehaviour
    {
        [SerializeField] private Transform unitParent;
        [SerializeField] private Vector3 firstUnitPosition;
        [SerializeField] private int amountOfColumns;
        [SerializeField] private float rowOffset;
        [SerializeField] private float columnOffset;
        
        public Zone Zone { get; private set; }
        
        private EventBus eventBus;
        private ZoneSettings zoneSettings;
        
        public void Initialize(EventBus eventBus, ZoneSettings zoneSettings, Zone zone)
        {
            this.eventBus = eventBus;
            this.zoneSettings = zoneSettings;
            Zone = zone;

            eventBus.Subscribe<ZoneAddUnitEvent>(HandleZoneAddUnitEvent);
            
            SpawnUnits(zone.AmountOfUnits, this.zoneSettings.UnitPrefab);
        }
        
        private void OnDestroy()
        {
            eventBus.Unsubscribe<ZoneAddUnitEvent>(HandleZoneAddUnitEvent);
        }

        private void HandleZoneAddUnitEvent(ZoneAddUnitEvent @event)
        {
            if (@event.Zone == Zone)
            {
                SpawnUnits(Zone.AmountOfUnits, zoneSettings.UnitPrefab);
            }
        }

        private void SpawnUnits(int amountOfUnits, GameObject unitPrefab)
        {
            ClearUnits();

            for (var i = 0; i < amountOfUnits; i++)
            {
                var unit = Instantiate(unitPrefab, unitParent);
                var offset = i % amountOfColumns * columnOffset * Vector3.right + i / amountOfColumns * rowOffset * Vector3.back;
                unit.transform.localPosition = firstUnitPosition + offset;
            }
        }

        private void ClearUnits()
        {
            for (var i = 0; i < unitParent.childCount; i++)
            {
                Destroy(unitParent.GetChild(i).gameObject);
            }
        }
    }
}