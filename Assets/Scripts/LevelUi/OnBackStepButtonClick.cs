using Common;
using Signals.Level;

namespace LevelUi
{
    public class OnBackStepButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnBackStepSignal>();
        }
    }
}