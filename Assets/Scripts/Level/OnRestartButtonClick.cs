using Common;
using Signals.Level;

namespace Level
{
    public class OnRestartButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnRestartSignal>();
        }
    }
}