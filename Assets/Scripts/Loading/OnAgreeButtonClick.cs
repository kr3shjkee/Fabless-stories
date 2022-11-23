using Signals.Loading;

namespace Loading
{
    public class OnAgreeButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnAgreeButtonClickSignal>();
        }
    }
}