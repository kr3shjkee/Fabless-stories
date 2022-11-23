namespace Signals.Loading
{
    public class OnMusicOptionChangedSignal
    {
        public readonly bool IsMute;
        public  OnMusicOptionChangedSignal(bool isMute)
        {
            IsMute = isMute;
        }
    }
}