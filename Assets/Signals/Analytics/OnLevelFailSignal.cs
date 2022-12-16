namespace Signals.Analytics
{
    public class OnLevelFailSignal
    {
        public readonly int LevelNumber;

        public OnLevelFailSignal(int levelNumber)
        {
            LevelNumber = levelNumber;
        }
    }
}