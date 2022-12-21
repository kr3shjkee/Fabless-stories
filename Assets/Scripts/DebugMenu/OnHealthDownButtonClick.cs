using Common;
using Signals.DebugMenu;

namespace DebugMenu
{
    public class OnHealthDownButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnHealthDownSignal>();
        }
    }
}