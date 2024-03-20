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
        }

        private void Awake()
        {
            eventBus.Subscribe<ZoneVisualSpawnedEvent>(HandleZoneVisualSpawnedEvent);
        }
        
        private void OnDestroy()
        {
            eventBus.Unsubscribe<ZoneVisualSpawnedEvent>(HandleZoneVisualSpawnedEvent);
        }

        private void HandleZoneVisualSpawnedEvent(ZoneVisualSpawnedEvent @event)
        {
            ClearButtons();
            foreach (var position in GetNewBuyZoneButtonsPositions())
            {
                Instantiate(buyZoneButtonPrefab, position, Quaternion.identity, transform);
            }
        }
        
        private void ClearButtons()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        private List<Vector3> GetNewBuyZoneButtonsPositions()
        {
            var result = new List<Vector3>();
            foreach (var zoneVisualPosition in zoneVisualPositionsHolder.ZoneVisualPositions)
            {
                foreach (var neighbourPosition in GetNeighbourPositions(zoneVisualPosition))
                {
                    if (zoneVisualPositionsHolder.ZoneVisualPositions.Contains(neighbourPosition))
                    {
                        continue;
                    }

                    if (result.Contains(neighbourPosition))
                    {
                        continue;
                    }
                    
                    result.Add(neighbourPosition);
                }
            }
            return result;
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
    }
}