using Common;
using Configs;
using UnityEngine;
using Zenject;

namespace Game
{
    public class ChapterMapController : IInitializable
    {
        private const float OFFSET = 5f;
        
        
        private readonly LevelMapConfig[] _levelMapConfigs;
        private readonly MapLevel.Factory _factory;
        private readonly SaveSystem _saveSystem;

        private MapLevel[] _levels;
        
        public MapLevel[] Levels => _levels;

        public ChapterMapController(ChapterMapConfig[] chapterMapConfig, MapLevel.Factory factory, SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _saveSystem.LoadData();
            _levelMapConfigs = chapterMapConfig[_saveSystem.Data.CurrentChapterNumber-1].LevelMapConfigs;
            _factory = factory;
        }
        
        public void Initialize()
        {
            GenerateMapLevels();
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