using System.Linq;
using Common;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ShopConfig", menuName = "Configs/Shop/ShopConfig", order = 0)]
    public class ShopConfig : ScriptableObject
    {
        [SerializeField] private ShopItemConfig[] shopItems;

        public ShopItemConfig[] ShopItems => shopItems;

        public ShopItemConfig GetItemById(string id)
        {
            return ShopItems.FirstOrDefault(item => item.ID == id);
        }
    }
}