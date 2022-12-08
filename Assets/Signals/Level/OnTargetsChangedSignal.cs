namespace Signals.Level
{
    public class OnTargetsChangedSignal
    {
        public readonly int Value;

        public OnTargetsChangedSignal(int value)
        {
            Value = value;
        }
    }
}