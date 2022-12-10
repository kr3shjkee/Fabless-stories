using Common;
using Signals.Ui;

namespace GameUi
{
    public class OnPlayLevelButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            if (_saveSystem.Data.IsGameEnded)
                return;
            
            base.OnClick();
            _signalBus.Fire<OnPlayLevelButtonClickSignal>();
        }
    }
}