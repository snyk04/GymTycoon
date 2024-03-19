using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Audio
{
    [CreateAssetMenu(menuName = "GymTycoon/ButtonAudioSettings", fileName = "ButtonAudioSettings", order = 0)]
    public sealed class ButtonAudioSettings : ScriptableObject
    {
        [SerializeField] private AudioClip[] pressAudioClips;

        public IReadOnlyList<AudioClip> PressAudioClips => pressAudioClips;
    }
}