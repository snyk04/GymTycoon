using System;
using UnityEngine;

namespace Code.Scripts.Zones
{
    public sealed class ZoneAmountOfUnitsIncreasedEvent
    {
        public Zone Zone { get; }
        
        public ZoneAmountOfUnitsIncreasedEvent(Zone zone)
        {
            Zone = zone;
        }
    }
    public sealed class Zone : MonoBehaviour
    {
        private EventBus eventBus;
        public ZoneSettings ZoneSettings { get; private set; }
        public int AmountOfUnits { get; private set; }
        private DateTime lastEarningTime;

        private bool isInitialized;
        
        private void Update()
        {
            if (!isInitialized)
            {
                return;
            }

            if ((DateTime.Now - lastEarningTime).TotalMilliseconds > ZoneSettings.CycleLengthInMs)
            {
                var amountOfResource = AmountOfUnits * ZoneSettings.ResourcePerCycle;
                var @event = new ZoneProducedResourceEvent(ZoneSettings.ResourceType, amountOfResource);
                eventBus.RaiseEvent(@event);
                lastEarningTime = DateTime.Now;
            }
        }

        public void Initialize(EventBus eventBus, ZoneSettings zoneSettings, int amountOfUnits)
        {
            this.eventBus = eventBus;
            this.ZoneSettings = zoneSettings;
            this.AmountOfUnits = amountOfUnits;

            isInitialized = true;
        }

        public void IncreaseAmountOfUnits()
        {
            if (AmountOfUnits + 1 > ZoneSettings.MaxUnits)
            {
                // TODO : Handle this situation
                return;
            }

            AmountOfUnits++;
        }
    }
}