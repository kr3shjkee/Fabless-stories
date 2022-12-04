using Common;
using Signals.Ui;

namespace GameUi
{
    public class OnPlayLevelButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnPlayLevelButtonClickSignal>();
        }
    }
}