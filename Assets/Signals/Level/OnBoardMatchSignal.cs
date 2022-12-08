using Level;

namespace Signals.Level
{
    public class OnBoardMatchSignal
    {
        public readonly string Key;
        public readonly int Count;
        public OnBoardMatchSignal(string key, int count)
        {
            Key = key;
            Count = count;
        }
    }
}