using Signals.Loading;

namespace Loading
{
    public class OnBackButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            _signalBus.Fire<OnBackButtonClickSignal>();
        }
    }
}