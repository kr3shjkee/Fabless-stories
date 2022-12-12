using Common;

namespace GameUi
{
    public class OnShopItemBuyClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnShopItemBuyClick>();
        }
    }
}