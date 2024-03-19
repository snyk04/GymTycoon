using System;
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
        
        private EventBus eventBus;
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

        public void Initialize(int id, ZoneSettings zoneSettings, int amountOfUnits, EventBus eventBus)
        {
            Id = id;
            ZoneSettings = zoneSettings;
            AmountOfUnits = amountOfUnits;
            this.eventBus = eventBus;

            isInitialized = true;
        }

        public void IncreaseAmountOfUnits()
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