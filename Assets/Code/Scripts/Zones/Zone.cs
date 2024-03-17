using System;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Zones
{
    public sealed class Zone : MonoBehaviour
    {
        private EventBus eventBus;
        private ZoneSettings zoneSettings;
        private int amountOfUnits;
        private DateTime lastEarningTime;

        private bool isInitialized;
        
        private void Update()
        {
            if (!isInitialized)
            {
                return;
            }

            if ((DateTime.Now - lastEarningTime).TotalMilliseconds > zoneSettings.CycleLengthInMs)
            {
                var amountOfResource = amountOfUnits * zoneSettings.ResourcePerCycle;
                var @event = new ZoneProducedResourceEvent(zoneSettings.ResourceType, amountOfResource);
                eventBus.RaiseEvent(@event);
                lastEarningTime = DateTime.Now;
            }
        }

        public void Initialize(EventBus eventBus, ZoneSettings zoneSettings, int amountOfUnits)
        {
            this.eventBus = eventBus;
            this.zoneSettings = zoneSettings;
            this.amountOfUnits = amountOfUnits;

            isInitialized = true;
        }

        public void IncreaseAmountOfUnits()
        {
            if (amountOfUnits + 1 > zoneSettings.MaxUnits)
            {
                // TODO : Handle this situation
                return;
            }

            amountOfUnits++;
        }
    }
}