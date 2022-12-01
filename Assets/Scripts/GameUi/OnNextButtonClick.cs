using Common;
using Signals.Ui;

namespace GameUi
{
    public class OnNextButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnNextButtonClickSignal>();
        }
    }
}