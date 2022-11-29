using Common;
using Signals.Loading;

namespace Loading
{
    public class OnOptionsButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnOptionsButtonClickSignal>();
        }
    }
}