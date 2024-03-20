using Code.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Code.Scripts.Audio
{
    public sealed class ButtonAudio : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Selectable selectable;

        private AudioManager audioManager;
        private ButtonAudioSettings audioSettings;

        [Inject]
        public void Construct(AudioManager audioManager, ButtonAudioSettings audioSettings)
        {
            this.audioManager = audioManager;
            this.audioSettings = audioSettings;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (selectable.interactable)
            {
                audioManager.PlayAudio(audioSettings.PressAudioClips.GetRandom());
            }
        }
    }
}