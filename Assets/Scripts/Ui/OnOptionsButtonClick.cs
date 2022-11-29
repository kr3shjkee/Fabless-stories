using Common;
using Signals.Ui;

namespace Ui
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