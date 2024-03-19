using System;
using Code.Scripts.Save.Interfaces;
using Code.Scripts.Zones.Events;

namespace Code.Scripts.Zones
{
    public sealed class ZoneAmountOfUnitsSaver : IDisposable
    {
        private readonly EventBus eventBus;
        private readonly IGameSaveManager gameSaveManager;
        
        public ZoneAmountOfUnitsSaver(EventBus eventBus, IGameSaveManager gameSaveManager)
        {
            this.eventBus = eventBus;
            this.gameSaveManager = gameSaveManager;
            
            eventBus.Subscribe<ZoneAmountOfUnitsIncreasedEvent>(HandleZoneAmountOfUnitsIncreasedEvent);
        }

        public void Dispose()
        {
            eventBus.Unsubscribe<ZoneAmountOfUnitsIncreasedEvent>(HandleZoneAmountOfUnitsIncreasedEvent);
        }
        
        private void HandleZoneAmountOfUnitsIncreasedEvent(ZoneAmountOfUnitsIncreasedEvent @event)
        {
            gameSaveManager.SaveData.ZoneSaveDataList[@event.Zone.Id].AmountOfUnits = @event.Zone.AmountOfUnits;
        }
    }
}