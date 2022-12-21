using Common;
using Signals.DebugMenu;

namespace DebugMenu
{
    public class OnLevelCompleteDebugButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnLevelCompleteDebugSignal>();
        }
    }
}