using UnityEngine;

namespace Common
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "Configs/Shop/ShopItemConfig", order = 0)]
    public class ShopItemConfig : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private Sprite sprite;
        [SerializeField] private uint priceValue;
        [SerializeField] private float cooldown;

        public string ID => id;

        public Sprite Sprite => sprite;

        public uint PriceValue => priceValue;

        public float Cooldown => cooldown;
    }
}