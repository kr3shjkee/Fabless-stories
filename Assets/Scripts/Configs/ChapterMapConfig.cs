using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ChapterMapConfig", menuName = "Configs/ChapterMapConfig", order = 0)]
    public class ChapterMapConfig : ScriptableObject
    {
        [SerializeField] private LevelMapConfig[] levelMapConfigs;

        public LevelMapConfig[] LevelMapConfigs => levelMapConfigs;
    }

    [Serializable]
    public class LevelMapConfig
    {
        [SerializeField] private int levelNumber;
        [SerializeField] private bool isDialog;
        [SerializeField] private Colors color;

        public bool IsDialog => isDialog;

        public int LevelNumber => levelNumber;
        
        private enum Colors
        {
            Red,
            Yellow,
            Blue,
            Green
        }

        public Color GetLevelColor()
        {
            Color _color = default;
            switch (color)
            {
                case Colors.Red:
                    _color = Color.red;
                    break;
                case Colors.Yellow:
                    _color = Color.yellow;
                    break;
                case Colors.Blue:
                    _color = Color.blue;
                    break;
                case Colors.Green:
                    _color = Color.green;
                    break;
            }
            return _color;
        }
    }
}