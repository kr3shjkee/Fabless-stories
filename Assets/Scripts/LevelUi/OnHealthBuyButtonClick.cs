using Common;
using Signals.Level;

namespace LevelUi
{
    public class OnHealthBuyButtonClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnHealthBuyButtonClick>();
        }
    }
}