using System;
using Configs;
using Game;
using Signals.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Button = UnityEngine.UI.Button;

namespace GameUi
{
    public class ShopItem : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<ItemConfig,  ShopItem>
        {
        }

        private const string COOLDOWN = "Cooldown:";
        
        [SerializeField] private Image selectedBg;
        [SerializeField] private Image itemSprite;
        [SerializeField] private TextMeshProUGUI priceValue;
        [SerializeField] private TextMeshProUGUI cooldownValue;

        private Button _button;
        private GameObject _parent;

        private SignalBus _signalBus;
        private ItemConfig _itemConfig;
        
        private bool _isLocked;
        private bool _isSelected;
        private int _price;

        public bool IsLocked => _isLocked;

        public bool IsSelected => _isSelected;

        public int Price => _price;

        [Inject]
        public void Construct(SignalBus signalBus, ItemConfig itemConfig)
        {
            _signalBus = signalBus;
            _itemConfig = itemConfig;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<OnInitShopItemsSignal>(Init);
        }
        
         
        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnInitShopItemsSignal>(Init);
            _button.onClick.RemoveListener(OnClick);
        }
        
        private void Init()
        {
            _parent = FindObjectOfType<ShopItemsParent>().gameObject;
            gameObject.transform.SetParent(_parent.transform);
            _button = GetComponentInChildren<Button>();
            
            _button.onClick.AddListener(OnClick);
            _price = (int)_itemConfig.PriceValue;
            priceValue.text = Price.ToString();
            cooldownValue.text = COOLDOWN + _itemConfig.CooldownInMinutes.ToString() + "m";
            selectedBg.gameObject.SetActive(false);
            itemSprite.sprite = _itemConfig.Sprite;
            _isSelected = false;
        }

        private void OnClick()
        {
            if (_isLocked)
                return;
            
            _signalBus.Fire(new OnShopElementClickSignal(this));
        }
        
        public void SetSelected(bool isSelected)
        {
            selectedBg.gameObject.SetActive(isSelected);
            _isSelected = isSelected;
        }

        public void DestroyItem()
        {
            Destroy(gameObject);
        }
    }
}