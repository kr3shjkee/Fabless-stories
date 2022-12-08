namespace Signals.Level
{
    public class OnHealthChangedSignal
    {
        public readonly int Value;

        public OnHealthChangedSignal(int value)
        {
            Value = value;
        }
    }
}