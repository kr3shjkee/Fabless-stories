using GameUi;

namespace Signals.Ui
{
    public class OnAddShopItemToListSignal
    {
        public readonly ShopItem Item;
        
        public OnAddShopItemToListSignal(ShopItem item)
        {
           Item = item;
        }
    }
}