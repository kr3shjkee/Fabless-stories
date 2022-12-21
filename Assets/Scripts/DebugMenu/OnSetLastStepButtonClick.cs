using Common;
using Signals.DebugMenu;

namespace DebugMenu
{
    public class OnSetLastStepButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnSetLastStepSignal>();
        }
    }
}