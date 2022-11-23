using Signals.Loading;

namespace Loading
{
    public class OnExitButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnExitButtonClickSignal>();
        }
    }
}