using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "ShopItemConfig", menuName = "Configs/ShopItemConfig", order = 0)]
    public class ShopItemConfig : ScriptableObject
    {
        [SerializeField] private ItemConfig[] items;

        public ItemConfig[] Items => items;
    }

    [System.Serializable]
    public class ItemConfig
    {
        [SerializeField] private string id;
        [SerializeField] private Sprite sprite;
        [SerializeField] private uint priceValue;
        [SerializeField] private float cooldownInMinutes;

        public string ID => id;

        public Sprite Sprite => sprite;

        public uint PriceValue => priceValue;

        public float CooldownInMinutes => cooldownInMinutes;
    }
}