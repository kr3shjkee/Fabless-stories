using Signals.Loading;

namespace Loading
{
    public class OnBackButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnBackButtonClickSignal>();
        }
    }
}