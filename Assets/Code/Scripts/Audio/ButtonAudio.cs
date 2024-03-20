using System.Collections.Generic;
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
            if (selectable is null)
            {
                Debug.Log("selectable is null");
                return;
            }
            
            if (audioSettings is null)
            {
                Debug.Log("audioSettings is null");
                return;
            }
            
            if (selectable.interactable)
            {
                PlayRandom(audioSettings.PressAudioClips);
            }
        }

        private void PlayRandom(IReadOnlyList<AudioClip> audioClips)
        {
            audioManager.PlayAudio(audioClips.GetRandom());
        }
    }
}