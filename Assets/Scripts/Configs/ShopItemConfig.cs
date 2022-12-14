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
        [SerializeField] private int id;
        [SerializeField] private Sprite sprite;
        [SerializeField] private int priceValue;
        [SerializeField] private float cooldownInMinutes;

        public int ID => id;

        public Sprite Sprite => sprite;

        public int PriceValue => priceValue;

        public float CooldownInMinutes => cooldownInMinutes;
    }
}