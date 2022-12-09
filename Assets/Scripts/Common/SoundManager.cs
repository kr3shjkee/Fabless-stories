using UnityEngine;

namespace Common
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource bgMusic;
        [SerializeField] private AudioSource buttonClickSound;
        [SerializeField] private AudioSource keyboardSound;
        [SerializeField] private AudioSource elementClickSound;
        [SerializeField] private AudioSource matchSound;

        public void IsMusicMute(bool value)
        {
            bgMusic.mute = value;
        }
        
        public void IsSoundMute(bool value)
        {
            buttonClickSound.mute = value;
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
    }
}