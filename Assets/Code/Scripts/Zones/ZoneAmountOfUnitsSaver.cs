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
            
            eventBus.Subscribe<ZoneAddUnitEvent>(HandleZoneAddUnit);
        }
        
        public void Dispose()
        {
            eventBus.Unsubscribe<ZoneAddUnitEvent>(HandleZoneAddUnit);
        }
        
        private void HandleZoneAddUnit(ZoneAddUnitEvent @event)
        {
            var zoneSaveData = gameSaveManager.SaveData.ZoneSaveDataList[@event.Zone.Id];
            zoneSaveData.AmountOfUnits = @event.Zone.AmountOfUnits;
        }
    }
}