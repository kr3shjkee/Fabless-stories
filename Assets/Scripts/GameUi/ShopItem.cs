using System;
using Common;
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
        [SerializeField] private Image bgForLock;
        [SerializeField] private TextMeshProUGUI priceValue;
        [SerializeField] private TextMeshProUGUI cooldownValue;

        private Button _button;

        private SignalBus _signalBus;
        private  ItemConfig _itemConfig;
        private SaveSystem _saveSystem;
        
        private bool _isLocked;
        private bool _isSelected;
        private int _price;
        private DateTime _targetTime;
        

        public bool IsSelected => _isSelected;

        public int Price => _price;

        [Inject]
        public void Construct(SignalBus signalBus, ItemConfig itemConfig, SaveSystem saveSystem)
        {
            _signalBus = signalBus;
            _itemConfig = itemConfig;
            _saveSystem = saveSystem;
        }

        private void Awake()
        {
            _signalBus.Subscribe<OnInitShopItemsSignal>(Init);
        }


        private void Update()
        {
            if (_isLocked)
            {
                CheckTime();
                TimeSpan time = _targetTime - DateTime.Now;
                var mins = time.Minutes + ":";
                var secs = time.Seconds.ToString();
                cooldownValue.text = mins + secs;
            }
        }
        
        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnInitShopItemsSignal>(Init);
            _button.onClick.RemoveListener(OnClick);
        }
        
        private void Init()
        {
            _saveSystem.LoadData();
            
            _button = GetComponentInChildren<Button>();
            _button.onClick.AddListener(OnClick);
            SetDefaultParams();
            
            if (_saveSystem.TimersData.ShopItemsTimers.ContainsKey(_itemConfig.ID))
                StartTimer();
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
            if (_saveSystem.TimersData.ShopItemsTimers.ContainsKey(_itemConfig.ID))
            {
                _saveSystem.TimersData.ShopItemsTimers.Remove(_itemConfig.ID);
                _saveSystem.TimersData.ShopItemsTimers.Add(_itemConfig.ID, _targetTime.ToString());
                _saveSystem.SaveData();
            }
            Destroy(gameObject);
        }

        private void SetDefaultParams()
        {
            _price = _itemConfig.PriceValue;
            priceValue.text = Price.ToString();
            cooldownValue.text = COOLDOWN + _itemConfig.CooldownInMinutes + "m";
            selectedBg.gameObject.SetActive(false);
            bgForLock.gameObject.SetActive(false);
            itemSprite.sprite = _itemConfig.Sprite;
            _isSelected = false;
            _isLocked = false;
        }
        
        private void DoLock()
        {
            _isLocked = true;
            bgForLock.gameObject.SetActive(true);
        }

        public void StartTimer()
        {
            if (_saveSystem.TimersData.ShopItemsTimers.ContainsKey(_itemConfig.ID))
            {
                _targetTime = DateTime.Parse(_saveSystem.TimersData.ShopItemsTimers[_itemConfig.ID]);
                DoLock();
                CheckTime();
            }
            else
            {
                var currentTime = DateTime.Now;
                _targetTime = currentTime.AddMinutes(_itemConfig.CooldownInMinutes);
                _saveSystem.TimersData.ShopItemsTimers.Add(_itemConfig.ID, _targetTime.ToString());
                _saveSystem.SaveData();
                DoLock();
            }
        }

        private void CheckTime()
        {
            var time = _targetTime - DateTime.Now;

            if (time.Seconds < 0)
            {
                _saveSystem.TimersData.ShopItemsTimers.Remove(_itemConfig.ID);
                _saveSystem.SaveData();
                SetDefaultParams();
            }
        }
    }
}