using Signals.Loading;

namespace Loading
{
    public class OnAgreeButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            _signalBus.Fire<OnAgreeButtonClickSignal>();
        }
    }
}