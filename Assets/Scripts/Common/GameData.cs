﻿namespace Common
{
    public class GameData
    {
        private const int DEFAULT_GOLD_VALUE = 1000;
        private const int DEFAULT_LEVEL_NUMBER = 0;
        private const bool DEFAULT_SOUND_MUTE = false;
        private const bool DEFAULT_MUSIC_MUTE = false;
        private const int DEFAULT_HEALTH_VALUE = 5;
        
        public int Gold;
        public int CurrentLevelNumber;
        public int HealthValue;
        public bool IsSoundMute;
        public bool IsMusicMute;

        public GameData()
        {
            Gold = DEFAULT_GOLD_VALUE;
            CurrentLevelNumber = DEFAULT_LEVEL_NUMBER;
            IsSoundMute = DEFAULT_SOUND_MUTE;
            IsMusicMute = DEFAULT_MUSIC_MUTE;
            HealthValue = DEFAULT_HEALTH_VALUE;
        }
    }
}