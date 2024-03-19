using Code.Scripts.Utils;
using Code.Scripts.Zones.Events;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Audio
{
    public sealed class BuyZoneClickedAudioPlayer : MonoBehaviour
    {
        [SerializeField] private ButtonAudioSettings audioSettings;

        private EventBus eventBus;
        private AudioManager audioManager;

        [Inject]
        private void Construct(EventBus eventBus, AudioManager audioManager)
        {
            this.eventBus = eventBus;
            this.audioManager = audioManager;
            
            eventBus.Subscribe<BuyZoneClickedEvent>(HandleBuyZoneClickedEvent);
        }

        private void OnDestroy()
        {
            eventBus.Unsubscribe<BuyZoneClickedEvent>(HandleBuyZoneClickedEvent);
        }
        
        private void HandleBuyZoneClickedEvent(BuyZoneClickedEvent _)
        {
            audioManager.PlayAudio(audioSettings.PressAudioClips.GetRandom());
        }
    }
}