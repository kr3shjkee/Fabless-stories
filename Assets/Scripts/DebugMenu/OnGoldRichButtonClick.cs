using Common;

namespace DebugMenu
{
    public class OnGoldRichButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnGoldRichSignal>();
        }
    }
}