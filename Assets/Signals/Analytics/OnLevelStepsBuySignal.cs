namespace Signals.Analytics
{
    public class OnLevelStepsBuySignal
    {
        public readonly int StepsCount;
        public readonly int GoldLost;
        
        public OnLevelStepsBuySignal(int steps, int gold)
        {
            StepsCount = steps;
            GoldLost = gold;
        }
    }
}