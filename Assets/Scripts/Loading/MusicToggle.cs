using Signals.Loading;

namespace Loading
{
    public class MusicToggle : BaseToggle
    {
        protected override void Start()
        {
            ChangeToggleValue(_saveSystem.Data.IsMusicMute);
        }

        public override void ChangeToggleValue()
        {
            base.ChangeToggleValue();
            _signalBus.Fire(new OnMusicOptionChangedSignal(_isMute));
            _soundManager.IsMusicMute(_isMute);
        }
    }
}