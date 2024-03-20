using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Audio
{
    public sealed class ButtonAudioSettings : MonoBehaviour
    {
        [SerializeField] private AudioClip[] pressAudioClips;

        public IReadOnlyList<AudioClip> PressAudioClips => pressAudioClips;
    }
}