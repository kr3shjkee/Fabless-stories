namespace Signals.Analytics
{
    public class OnLevelCompleteSignal
    {
        public readonly int LevelNumber;
        public readonly int NonUsedSteps;

        public OnLevelCompleteSignal(int levelNumber, int nonUsedSteps)
        {
            LevelNumber = levelNumber;
            NonUsedSteps = nonUsedSteps;
        }
    }
}