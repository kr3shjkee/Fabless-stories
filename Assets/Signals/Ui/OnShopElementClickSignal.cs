using GameUi;

namespace Signals.Ui
{
    public class OnShopElementClickSignal
    {
        public readonly ShopItem Item;
        
        public OnShopElementClickSignal(ShopItem item)
        {
           Item = item;
        }
    }
}