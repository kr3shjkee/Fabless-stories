using Signals.Loading;

namespace Loading
{
    public class OnOptionsButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            _signalBus.Fire<OnOptionsButtonClickSignal>();
        }
    }
}