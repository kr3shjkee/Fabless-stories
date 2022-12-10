using System;
using Common;
using Configs;
using Signals.Game;
using UnityEngine;
using Zenject;

namespace Game
{
    public class ChapterMapController : IInitializable, IDisposable
    {
        private const float OFFSET = 5f;
        
        
        private readonly LevelMapConfig[] _levelMapConfigs;
        private readonly MapLevel.Factory _factory;
        private readonly SignalBus _signalBus;
        private readonly SaveSystem _saveSystem;

        private MapLevel[] _levels;
        
        public MapLevel[] Levels => _levels;

        public ChapterMapController(ChapterMapConfig[] chapterMapConfig, MapLevel.Factory factory, SignalBus signalBus, SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _saveSystem.LoadData();
            _levelMapConfigs = chapterMapConfig[_saveSystem.Data.CurrentChapterNumber-1].LevelMapConfigs;
            _factory = factory;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            GenerateMapLevels();
            //_signalBus.Fire<EndLevelMapInitializeSignal>();
        }

        public void Dispose()
        {
            
        }

        private void GenerateMapLevels()
        {
            _levels = new MapLevel[_levelMapConfigs.Length];
            Vector2 startPos = new Vector2(0, 0);

            for (int i = 0; i < _levelMapConfigs.Length; i++)
            {
                var pos = startPos + new Vector2(0, i * OFFSET);
                var level = _factory.Create(_levelMapConfigs[i], new MapLevelPosition(pos));
                _levels[i] = level;
                level.Initialize();
            }
        }
    }
}