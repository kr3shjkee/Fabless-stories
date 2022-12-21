using Common;
using Signals.DebugMenu;

namespace DebugMenu
{
    public class OnMoveToNextLevelButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnMoveToNextLevelSignal>();
        }
    }
}