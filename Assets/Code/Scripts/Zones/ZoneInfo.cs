using System;
using Code.Scripts.Resources;
using Code.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Scripts.Zones
{
    public sealed class ZoneInfo : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text amountOfUnitsText;
        [SerializeField] private Button increaseAmountOfUnitsButton;
        [SerializeField] private TMP_Text increaseAmountOfUnitsButtonText;

        private EventBus eventBus;
        private ResourcesHolder resourcesHolder;

        private Zone currentZone;
        private ZoneSettings CurrentZoneSettings => currentZone.ZoneSettings;

        [Inject]
        private void Construct(EventBus eventBus, ResourcesHolder resourcesHolder)
        {
            this.eventBus = eventBus;
            this.resourcesHolder = resourcesHolder;
            
            eventBus.Subscribe<ZoneVisualClickedEvent>(HandleZoneVisualClickedEvent);
        }

        private void Awake()
        {
            increaseAmountOfUnitsButton.onClick.AddListener(IncreaseAmountOfUnits);
        }

        private void IncreaseAmountOfUnits()
        {
            if (currentZone == null)
            {
                return;
            }
            
            resourcesHolder.ChangeResource(CurrentZoneSettings.ResourceType, -CurrentZoneSettings.ResourcePerNewUnit);
            currentZone.IncreaseAmountOfUnits();
            amountOfUnitsText.text = GetAmountOfUnitsText(currentZone);
        }

        private void HandleZoneVisualClickedEvent(ZoneVisualClickedEvent @event)
        {
            canvasGroup.SetActive(true);

            currentZone = @event.ZoneVisual.Zone;

            titleText.text = CurrentZoneSettings.Title;
            amountOfUnitsText.text = GetAmountOfUnitsText(@event.ZoneVisual.Zone);
            increaseAmountOfUnitsButtonText.text = CurrentZoneSettings.ResourcePerNewUnit.ToString();
        }

        private string GetAmountOfUnitsText(Zone zone)
        {
            return $"Units: {zone.AmountOfUnits}/{zone.ZoneSettings.MaxUnits}";
        }

        private void Update()
        {
            if (currentZone == null)
            {
                return;
            }

            var amountOfResource = resourcesHolder.GetResource(CurrentZoneSettings.ResourceType);
            var enoughResource = amountOfResource > CurrentZoneSettings.ResourcePerNewUnit;
            var newUnitsCanBeBought = currentZone.AmountOfUnits < CurrentZoneSettings.MaxUnits;
            increaseAmountOfUnitsButton.interactable = enoughResource && newUnitsCanBeBought;
        }

        private void OnDestroy()
        {
            eventBus.Unsubscribe<ZoneVisualClickedEvent>(HandleZoneVisualClickedEvent);
            increaseAmountOfUnitsButton.onClick.RemoveListener(IncreaseAmountOfUnits);
        }
    }
}