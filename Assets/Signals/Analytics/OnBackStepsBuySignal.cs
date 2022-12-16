namespace Signals.Analytics
{
    public class OnBackStepsBuySignal
    {
        public readonly int BackStepsCount;
        public readonly int GoldLost;
        
        public OnBackStepsBuySignal(int backSteps, int gold)
        {
            BackStepsCount = backSteps;
            GoldLost = gold;
        }
    }
}