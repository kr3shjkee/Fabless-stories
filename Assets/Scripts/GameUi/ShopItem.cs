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
        public class Factory : PlaceholderFactory<ItemConfig, ShopItem>
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
        public  ItemConfig _itemConfig;
        
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
            _signalBus.Subscribe<OnSetDefaultItemSignal>(SetDefaultParamsFromSignal);
            _signalBus.Subscribe<DoLockShopItemSignal>(DoLock);
        }
        
         
        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnInitShopItemsSignal>(Init);
            _signalBus.Unsubscribe<OnSetDefaultItemSignal>(SetDefaultParamsFromSignal);
            _signalBus.Unsubscribe<DoLockShopItemSignal>(DoLock);
            _button.onClick.RemoveListener(OnClick);
        }
        
        private void Init()
        {
            _parent = FindObjectOfType<ShopItemsParent>().gameObject;
            gameObject.transform.SetParent(_parent.transform);
            _button = GetComponentInChildren<Button>();
            
            _button.onClick.AddListener(OnClick);
            SetDefaultParams();
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

        private void SetDefaultParams()
        {
            _price = (int)_itemConfig.PriceValue;
            priceValue.text = Price.ToString();
            cooldownValue.text = COOLDOWN + _itemConfig.CooldownInMinutes + "m";
            selectedBg.gameObject.SetActive(false);
            itemSprite.sprite = _itemConfig.Sprite;
            _isSelected = false;
            _isLocked = false;
        }

        private void SetDefaultParamsFromSignal(OnSetDefaultItemSignal signal)
        {
            if (signal.Key.ToString() == _itemConfig.ID)
            {
                cooldownValue.text = COOLDOWN + _itemConfig.CooldownInMinutes + "m";
                _isLocked = false;
            }
        }

        private void DoLock(DoLockShopItemSignal signal)
        {
            if (_itemConfig.ID == signal.Key.ToString())
                _isLocked = true;
        }
    }
}