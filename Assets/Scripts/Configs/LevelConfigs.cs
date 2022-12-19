using System.Linq;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "LevelConfigs", menuName = "Configs/LevelConfigs", order = 0)]
    public class LevelConfigs : ScriptableObject
    {
        [SerializeField] private LevelCfg[] levels;
        [SerializeField] private Target[] targets;

        public LevelCfg GetLevelConfigByNumber(int levelNumber)
        {
            return levels.FirstOrDefault(level => level.LevelNumber == levelNumber);
        }

        public Target GetTargetByKey(LevelCfg levelCfg)
        {
            var key = levelCfg.GetTarget();
            return targets.FirstOrDefault(target => target.Key == key);
        }
    }

    [System.Serializable]
    public class LevelCfg
    {
        [SerializeField] private int levelNumber;
        [SerializeField] private Target target;
        [SerializeField] private int targetCount;
        [SerializeField] private int stepsCount;

        private enum Target
        {
            Blue,
            Red,
            Yellow,
            Green,
            Purple
        }

        public int LevelNumber => levelNumber;

        public int StepsCount => stepsCount;

        public int TargetCount => targetCount;

        public string GetTarget()
        {
            string key = "";
            switch (target)
            {
                case Target.Blue:
                    key = Target.Blue.ToString();
                    break;
                case Target.Red:
                    key = Target.Red.ToString();
                    break;
                case Target.Yellow:
                    key = Target.Yellow.ToString();
                    break;
                case Target.Green:
                    key = Target.Green.ToString();
                    break;
                case Target.Purple:
                    key = Target.Purple.ToString();
                    break;
            }

            return key;
        }
    }

    [System.Serializable]
    public class Target
    {
        [SerializeField] private string key;
        [SerializeField] private Sprite sprite;


        public string Key => key;

        public Sprite Sprite => sprite;
    }
}