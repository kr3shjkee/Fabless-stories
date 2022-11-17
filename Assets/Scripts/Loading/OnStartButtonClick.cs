using Signals.Loading;

namespace Loading
{
    public class OnStartButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            _signalBus.Fire<OnStartButtonClickSignal>();
        }
    }
}