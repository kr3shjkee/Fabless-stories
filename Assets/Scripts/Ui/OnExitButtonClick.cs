using Common;
using Signals.Ui;

namespace Ui
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