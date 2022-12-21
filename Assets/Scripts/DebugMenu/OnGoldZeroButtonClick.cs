using Common;

namespace DebugMenu
{
    public class OnGoldZeroButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnGoldZeroSignal>();
        }
    }
}