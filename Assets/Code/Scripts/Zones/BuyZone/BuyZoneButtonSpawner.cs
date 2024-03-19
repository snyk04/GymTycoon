using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Zones.Events;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Zones.BuyZone
{
    public sealed class BuyZoneButtonSpawner : MonoBehaviour
    {
        [SerializeField] private BuyZoneButton buyZoneButtonPrefab;
        [SerializeField] private float offset;
        
        private EventBus eventBus;
        private ZoneVisualPositionsHolder zoneVisualPositionsHolder;
            
        [Inject]
        private void Construct(EventBus eventBus, ZoneVisualPositionsHolder zoneVisualPositionsHolder)
        {
            this.eventBus = eventBus;
            this.zoneVisualPositionsHolder = zoneVisualPositionsHolder;
            
            eventBus.Subscribe<ZoneVisualSpawnedEvent>(HandleZoneVisualSpawnedEvent);
        }

        private void HandleZoneVisualSpawnedEvent(ZoneVisualSpawnedEvent @event)
        {
            ClearButtons();
            foreach (var position in GetNewBuyZoneButtonsPositions())
            {
                Instantiate(buyZoneButtonPrefab, position, Quaternion.identity, transform);
            }
        }

        private List<Vector3> GetNewBuyZoneButtonsPositions()
        {
            var newBuyZoneButtonsPositions = new List<Vector3>();
            foreach (var zoneVisualPosition in zoneVisualPositionsHolder.ZoneVisualPositions)
            {
                foreach (var neighbourPosition in GetNeighbourPositions(zoneVisualPosition))
                {
                    if (zoneVisualPositionsHolder.ZoneVisualPositions.Contains(neighbourPosition))
                    {
                        continue;
                    }

                    if (newBuyZoneButtonsPositions.Contains(neighbourPosition))
                    {
                        continue;
                    }
                    
                    newBuyZoneButtonsPositions.Add(neighbourPosition);
                }
            }

            return newBuyZoneButtonsPositions;
        }

        private void ClearButtons()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        private IEnumerable<Vector3> GetNeighbourPositions(Vector3 position)
        {
            return new[]
            {
                position + Vector3.left * offset,
                position + Vector3.right * offset,
                position + Vector3.forward * offset,
                position + Vector3.back * offset
            };
        }

        private void OnDestroy()
        {
            eventBus.Unsubscribe<ZoneVisualSpawnedEvent>(HandleZoneVisualSpawnedEvent);
        }
    }
}