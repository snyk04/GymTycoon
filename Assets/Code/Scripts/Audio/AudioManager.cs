using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Code.Scripts.Utils;
using UnityEngine;

namespace Code.Scripts.Audio
{
    public sealed class AudioManager
    {
        private readonly Queue<AudioSource> freeAudioSources = new();
        private readonly HashSet<AudioSource> playingAudioSources = new();

        public void PlayAudio(AudioClip audioClip, bool isLooped = false)
        {
            var audioSource = GetOrCreateAudioSource();

            audioSource.clip = audioClip;
            audioSource.loop = isLooped;

            audioSource.Play();
            playingAudioSources.Add(audioSource);

            var isFinished = Box.From(false);

            if (!isLooped)
            {
                WaitAndTryDisableAudioSource(audioClip.length, audioSource, isFinished).CatchAndLog();
            }
        }

        private AudioSource GetOrCreateAudioSource()
        {
            if (freeAudioSources.TryDequeue(out var audioSource))
            {
                return audioSource;
            }
            
            var gameObject = new GameObject("Pooled AudioSource");

            gameObject.transform.SetParent(UnityEngine.Camera.main!.transform);
            gameObject.transform.localPosition = Vector3.zero;

            audioSource = gameObject.AddComponent<AudioSource>();

            return audioSource;
        }

        private async Task WaitAndTryDisableAudioSource(float length, AudioSource audioSource, Box<bool> isFinished)
        {
            await Task.Delay(TimeSpan.FromSeconds(length));

            TryDisableAudioSource(audioSource, isFinished);
        }

        private void TryDisableAudioSource(AudioSource audioSource, Box<bool> isFinished)
        {
            if (isFinished.Value || !playingAudioSources.Remove(audioSource) || audioSource == null)
            {
                return;
            }

            isFinished.Value = true;
            audioSource.Stop();
            audioSource.clip = null;
            freeAudioSources.Enqueue(audioSource);
        }
    }
}