using Common;
using Signals.Ui;

namespace Ui
{
    public class OnHealthButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnHealthButtonClickSignal>();
        }
    }
}