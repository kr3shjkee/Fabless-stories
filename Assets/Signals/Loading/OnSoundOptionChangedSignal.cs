namespace Signals.Loading
{
    public class OnSoundOptionChangedSignal
    {
        public readonly bool IsMute;
        public  OnSoundOptionChangedSignal(bool isMute)
        {
            IsMute = isMute;
        }
    }
}