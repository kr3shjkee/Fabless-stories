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
        
        public bool isLocked;

        [Inject]
        public void Construct(SignalBus signalBus, ItemConfig itemConfig)
        {
            _signalBus = signalBus;
            _itemConfig = itemConfig;
        }

        private void Start()
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
            priceValue.text = _itemConfig.PriceValue.ToString();
            cooldownValue.text = COOLDOWN + _itemConfig.CooldownInMinutes.ToString() + "m";
            selectedBg.gameObject.SetActive(false);
            itemSprite.sprite = _itemConfig.Sprite;
            _signalBus.Fire(new OnAddShopItemToListSignal(this));
        }

        private void OnClick()
        {
            Debug.Log(_itemConfig.ID);
        }
        
        public void SetSelected(bool isSelected)
        {
            selectedBg.enabled = isSelected;
        }


    }
}