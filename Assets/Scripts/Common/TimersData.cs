using System.Collections.Generic;

namespace Common
{
    public class TimersData
    {
        public Dictionary<int, string> ShopItemsTimers;
        
        public TimersData()
        {
            ShopItemsTimers = new Dictionary<int, string>();
        }
    }
}