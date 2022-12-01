using Common;
using Signals.Ui;

namespace GameUi
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