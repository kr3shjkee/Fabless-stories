using UnityEngine;

namespace Common
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource bgMusic;
        [SerializeField] private AudioSource buttonClickSound;

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
    }
}