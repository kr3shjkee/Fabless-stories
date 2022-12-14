using Common;
using Signals.Ui;

namespace GameUi
{
    public class OnSoundOptionsButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnSoundOptionsButtonClickSignal>();
        }
    }
}