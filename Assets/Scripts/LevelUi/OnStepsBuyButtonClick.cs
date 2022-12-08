using Common;
using Signals.Level;

namespace LevelUi
{
    public class OnStepsBuyButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnStepsRestoredSignal>();
        }
    }
}