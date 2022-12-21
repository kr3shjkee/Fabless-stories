using Common;

namespace DebugMenu
{
    public class OnHealthFullButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnHealthFullSignal>();
        }
    }
}