using Code.Scripts.Resources;
using Code.Scripts.Utils;
using Code.Scripts.Zones.Events;
using Code.Scripts.Zones.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Scripts.Zones
{
    public sealed class ZoneInfoPopup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text unitsText;
        [SerializeField] private Button addUnitButton;
        [SerializeField] private TMP_Text addUnitButtonText;

        private EventBus eventBus;
        private ResourcesHolder resourcesHolder;

        private Zone currentZone;
        private ZoneSettings CurrentZoneSettings => currentZone.ZoneSettings;

        [Inject]
        private void Construct(EventBus eventBus, ResourcesHolder resourcesHolder)
        {
            this.eventBus = eventBus;
            this.resourcesHolder = resourcesHolder;
        }

        private void Awake()
        {
            eventBus.Subscribe<ZoneVisualClickedEvent>(HandleZoneVisualClickedEvent);
            addUnitButton.onClick.AddListener(AddUnit);
        }
        
        private void OnDestroy()
        {
            eventBus.Unsubscribe<ZoneVisualClickedEvent>(HandleZoneVisualClickedEvent);
            addUnitButton.onClick.RemoveListener(AddUnit);
        }
        
        private void HandleZoneVisualClickedEvent(ZoneVisualClickedEvent @event)
        {
            canvasGroup.SetActive(true);

            currentZone = @event.ZoneVisual.Zone;

            titleText.text = CurrentZoneSettings.Title;
            unitsText.text = GetUnitsText(@event.ZoneVisual.Zone);
            addUnitButtonText.text = CurrentZoneSettings.ResourcePerNewUnit.ToString();
        }

        private string GetUnitsText(Zone zone)
        {
            return $"Units: {zone.AmountOfUnits}/{Zone.MaxUnits}";
        }

        private void AddUnit()
        {
            if (currentZone == null)
            {
                return;
            }
            
            resourcesHolder.ChangeResource(CurrentZoneSettings.ResourceType, -CurrentZoneSettings.ResourcePerNewUnit);
            currentZone.AddUnit();
            eventBus.RaiseEvent(new ZoneAddUnitEvent(currentZone));
            
            unitsText.text = GetUnitsText(currentZone);
        }

        private void Update()
        {
            if (currentZone == null)
            {
                return;
            }
            
            addUnitButton.interactable = EnoughResource() && CanAddUnit();
        }

        private bool EnoughResource()
        {
            var resource = resourcesHolder.GetResource(CurrentZoneSettings.ResourceType);
            return resource > CurrentZoneSettings.ResourcePerNewUnit;
        }

        private bool CanAddUnit()
        {
            return currentZone.AmountOfUnits < Zone.MaxUnits;
        }
    }
}