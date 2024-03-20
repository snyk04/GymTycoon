using Code.Scripts.Utils;
using Code.Scripts.Zones.Events;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Audio
{
    public sealed class BuyZoneClickedAudioPlayer : MonoBehaviour
    {
        private EventBus eventBus;
        private AudioManager audioManager;
        private ButtonAudioSettings audioSettings;

        [Inject]
        private void Construct(EventBus eventBus, AudioManager audioManager, ButtonAudioSettings audioSettings)
        {
            this.eventBus = eventBus;
            this.audioManager = audioManager;
            this.audioSettings = audioSettings;
            
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