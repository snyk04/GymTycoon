using System.Collections.Generic;
using Code.Scripts.Resources;
using Code.Scripts.Utils;
using Code.Scripts.Zones.Events;
using Code.Scripts.Zones.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Scripts.Zones.BuyZone
{
    public sealed class BuyZonePopup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_Dropdown zoneTypeDropdown;
        [SerializeField] private Button buyZoneButton;
        [SerializeField] private TMP_Text buyZoneButtonText;
        
        private EventBus eventBus;
        private ZoneSettingsHolder zoneSettingsHolder;
        private ResourcesHolder resourcesHolder;

        private Vector3 selectedZonePosition;
        private ZoneSettings selectedZoneSettings;
        
        [Inject]
        private void Construct(EventBus eventBus, ZoneSettingsHolder zoneSettingsHolder, 
            ResourcesHolder resourcesHolder)
        {
            this.eventBus = eventBus;
            this.zoneSettingsHolder = zoneSettingsHolder;
            this.resourcesHolder = resourcesHolder;
        }

        private void Awake()
        {
            eventBus.Subscribe<BuyZoneClickedEvent>(HandleBuyZoneClickedEvent);
            zoneTypeDropdown.onValueChanged.AddListener(ZoneTypeDropdownOnValueChanged);
            buyZoneButton.onClick.AddListener(BuyZoneButtonOnClick);
        }

        private void OnDestroy()
        {
            eventBus.Unsubscribe<BuyZoneClickedEvent>(HandleBuyZoneClickedEvent);
            zoneTypeDropdown.onValueChanged.RemoveListener(ZoneTypeDropdownOnValueChanged);
            buyZoneButton.onClick.RemoveListener(BuyZoneButtonOnClick);
        }
        
        private void HandleBuyZoneClickedEvent(BuyZoneClickedEvent @event)
        {
            selectedZonePosition = @event.Position;
            canvasGroup.SetActive(true);
        }
        
        private void ZoneTypeDropdownOnValueChanged(int zoneType)
        {
            selectedZoneSettings = zoneSettingsHolder.ZoneSettingsByZoneTypes[(ZoneType)zoneType];
            buyZoneButtonText.text = selectedZoneSettings.ResourcePerNewZone.ToString();
        }
        
        private void BuyZoneButtonOnClick()
        {
            var zoneType = (ZoneType)zoneTypeDropdown.value;
            eventBus.RaiseEvent(new ZoneBoughtEvent(zoneType, selectedZonePosition));
            resourcesHolder.ChangeResource(selectedZoneSettings.ResourceType, -selectedZoneSettings.ResourcePerNewZone);
            canvasGroup.SetActive(false);
        }

        private void Start()
        {
            zoneTypeDropdown.options = GetDropdownOptionDataList();
            ZoneTypeDropdownOnValueChanged(0);
        }

        private List<TMP_Dropdown.OptionData> GetDropdownOptionDataList()
        {
            var optionData = new List<TMP_Dropdown.OptionData>();
            foreach (var (_, zoneSettings) in zoneSettingsHolder.ZoneSettingsByZoneTypes)
            {
                optionData.Add(new TMP_Dropdown.OptionData(zoneSettings.Title));
            }

            return optionData;
        }

        private void Update()
        {
            buyZoneButton.interactable = SelectedZoneCanBeBought();
        }

        private bool SelectedZoneCanBeBought()
        {
            var resource = resourcesHolder.GetResource(selectedZoneSettings.ResourceType);
            return resource >= selectedZoneSettings.ResourcePerNewZone;
        }
    }
}