using Code.Scripts.Zones.Events;
using Code.Scripts.Zones.Models;
using UnityEngine;

namespace Code.Scripts.Zones
{
    public sealed class Zone : MonoBehaviour
    {
        public const int MaxUnits = 10;
        
        public int Id { get; private set; }
        public ZoneSettings ZoneSettings { get; private set; }
        public int AmountOfUnits { get; private set; }
        private Vector3 position;
        private float lastEarningTime;
        private bool isInitialized;
        
        private EventBus eventBus;

        public void Initialize(int id, ZoneSettings settings, int amountOfUnits, EventBus eventBus, Vector3 position)
        {
            Id = id;
            ZoneSettings = settings;
            AmountOfUnits = amountOfUnits;
            this.eventBus = eventBus;
            this.position = position;

            isInitialized = true;
        }
        
        private void Update()
        {
            if (!isInitialized)
            {
                return;
            }

            if (Time.time - lastEarningTime <= ZoneSettings.CycleLengthInSeconds)
            {
                return;
            }

            ProduceResource();
        }

        private void ProduceResource()
        {
            var amountOfResource = AmountOfUnits * ZoneSettings.ResourcePerCycle;
            eventBus.RaiseEvent(new ZoneProducedResourceEvent(ZoneSettings.ResourceType, amountOfResource, position));
            lastEarningTime = Time.time;
        }

        public void AddUnit()
        {
            if (AmountOfUnits + 1 > MaxUnits)
            {
                // TODO : Handle this situation
                return;
            }

            AmountOfUnits++;
        }
    }
}