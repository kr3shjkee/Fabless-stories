using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Common
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource bgMusic;
        [SerializeField] private AudioSource buttonClickSound;
        [SerializeField] private AudioSource keyboardSound;
        [SerializeField] private AudioSource elementClickSound;
        [SerializeField] private AudioSource matchSound;
        [SerializeField] private AudioSource winSound;
        [SerializeField] private GameObject soundParent;

        private SaveSystem _saveSystem;
        
        [Inject]
        public void Construct(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }
        

        public void IsMusicMute(bool value)
        {
            bgMusic.mute = value;
        }
        
        public void IsSoundMute(bool value)
        {
            foreach (var sound in 
                     soundParent.transform.GetComponentsInChildren<AudioSource>())
            {
                sound.mute = value;
            }
        }
        
        public void ButtonClickSoundPlay()
        {
            buttonClickSound.Play();
        }

        public void KeyboardSoundPlay()
        {
            keyboardSound.Play();
        }

        public void KeyboardSoundStop()
        {
            keyboardSound.Stop();
        }

        public void ElementClickSoundPlay()
        {
            elementClickSound.Play();
        }

        public void MatchSoundPlay()
        {
            matchSound.Play();
        }

        public async void WinSoundPlay()
        {
            if (!_saveSystem.Data.IsMusicMute)
            {
                IsMusicMute(true);
                winSound.Play();
                await UniTask.Delay(TimeSpan.FromSeconds(winSound.time));
                IsMusicMute(false);
            }
            else
                winSound.Play();
        }
    }
}