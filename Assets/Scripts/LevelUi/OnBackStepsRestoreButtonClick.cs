using Common;
using Signals.Level;

namespace LevelUi
{
    public class OnBackStepsRestoreButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnBackStepsRestoredSignal>();
        }
    }
}