using Signals.Loading;

namespace Loading
{
    public class OnStartButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnStartButtonClickSignal>();
        }
    }
}