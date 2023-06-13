using System;
using UnityEngine;

namespace EasyAudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;

        public static Action<string> PlayAudio;
        public static Action StopAudio;

        [HideInInspector] public Sound mainStart;
        private Sound mainLoop;
        private Sound deathScreen;

        private void Awake()
        {
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
            }
        }

        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Play();
        }

        public void Stop()
        {
            foreach(Sound s in sounds)
            {
                s.source.Stop();
            }
        }

        private void OnEnable()
        {
            PlayAudio += Play;
            StopAudio += Stop;
        }
        private void OnDisable()
        {
            PlayAudio -= Play;
            StopAudio -= Stop;
        }
    }
}