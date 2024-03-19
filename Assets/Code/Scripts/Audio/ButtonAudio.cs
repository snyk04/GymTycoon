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
        [SerializeField] private ButtonAudioSettings audioSettings;

        private AudioManager audioManager;

        [Inject]
        public void Construct(AudioManager audioManager)
        {
            this.audioManager = audioManager;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
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