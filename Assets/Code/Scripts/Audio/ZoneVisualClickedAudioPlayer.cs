using Code.Scripts.Utils;
using Code.Scripts.Zones.Events;
using UnityEngine;
using Zenject;

namespace Code.Scripts.Audio
{
    public sealed class ZoneVisualClickedAudioPlayer : MonoBehaviour
    {
        [SerializeField] private ButtonAudioSettings audioSettings;

        private EventBus eventBus;
        private AudioManager audioManager;

        [Inject]
        private void Construct(EventBus eventBus, AudioManager audioManager)
        {
            this.eventBus = eventBus;
            this.audioManager = audioManager;
            
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