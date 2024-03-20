using Code.Scripts.Utils;
using Code.Scripts.Zones.Events;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Audio
{
    public sealed class ZoneVisualClickedAudioPlayer : MonoBehaviour
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
        }

        private void Awake()
        {
            eventBus.Subscribe<ZoneVisualClickedEvent>(HandleZoneVisualClickedEvent);
        }

        private void OnDestroy()
        {
            eventBus.Unsubscribe<ZoneVisualClickedEvent>(HandleZoneVisualClickedEvent);
        }
        
        private void HandleZoneVisualClickedEvent(ZoneVisualClickedEvent _)
        {
            audioManager.PlayAudio(audioSettings.PressAudioClips.GetRandom());
        }
    }
}