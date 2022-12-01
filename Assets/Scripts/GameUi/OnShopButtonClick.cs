using Common;
using Signals.Ui;

namespace GameUi
{
    public class OnShopButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnShopButtonClickSignal>();
        }
    }
}