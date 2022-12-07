﻿namespace Common
{
    public class GameData
    {
        private const int DEFAULT_GOLD_VALUE = 1000;
        private const int DEFAULT_CHAPTER_NUMBER = 0;
        private const int DEFAULT_LEVEL_NUMBER = 1;
        private const bool DEFAULT_SOUND_MUTE = false;
        private const bool DEFAULT_MUSIC_MUTE = false;
        private const int DEFAULT_HEALTH_VALUE = 5;
        private const int DEFAULT_BACKSTEPS_VALUE = 5;
        
        public int Gold;
        public int CurrentChapterNumber;
        public int CurrentLevelNumber;
        public int HealthValue;
        public int CurrentBackStepsCount;
        public bool IsSoundMute;
        public bool IsMusicMute;

        public GameData()
        {
            Gold = DEFAULT_GOLD_VALUE;
            CurrentChapterNumber = DEFAULT_CHAPTER_NUMBER;
            CurrentLevelNumber = DEFAULT_LEVEL_NUMBER;
            CurrentBackStepsCount = DEFAULT_BACKSTEPS_VALUE;
            IsSoundMute = DEFAULT_SOUND_MUTE;
            IsMusicMute = DEFAULT_MUSIC_MUTE;
            HealthValue = DEFAULT_HEALTH_VALUE;
        }
    }
}