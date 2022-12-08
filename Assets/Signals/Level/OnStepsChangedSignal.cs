namespace Signals.Level
{
    public class OnStepsChangedSignal
    {
        public readonly int Value;

        public OnStepsChangedSignal(int value)
        {
            Value = value;
        }
    }
}