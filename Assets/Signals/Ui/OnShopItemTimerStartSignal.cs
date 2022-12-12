namespace Signals.Ui
{
    public class OnShopItemTimerStartSignal
    {
        public readonly int Key;
        public readonly int TimerValue;

        public OnShopItemTimerStartSignal(int key, int value)
        {
            Key = key;
            TimerValue = value;
        }
    }
}