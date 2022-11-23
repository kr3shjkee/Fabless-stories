using Signals.Loading;

namespace Loading
{
    public class SoundToggle : BaseToggle
    {
        protected override void Start()
        {
            ChangeToggleValue(_saveSystem.Data.IsSoundMute);
        }

        public override void ChangeToggleValue()
        {
            base.ChangeToggleValue();
            _signalBus.Fire(new OnSoundOptionChangedSignal(_isMute));
            _soundManager.IsSoundMute(_isMute);
        }
    }
}