﻿using System;
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
        private GameObject _parent;

        private SignalBus _signalBus;
        public  ItemConfig _itemConfig;
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

        private void OnEnable()
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
            _parent = FindObjectOfType<ShopItemsParent>().gameObject;
            gameObject.transform.SetParent(_parent.transform);
            _button = GetComponentInChildren<Button>();
            _saveSystem.LoadData();
            _button.onClick.AddListener(OnClick);
            SetDefaultParams();
            
            if (_saveSystem.Data.ShopItemsTimers.ContainsKey(Int32.Parse(_itemConfig.ID)))
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
            if (_saveSystem.Data.ShopItemsTimers.ContainsKey(Int32.Parse(_itemConfig.ID)))
                _saveSystem.Data.ShopItemsTimers[Int32.Parse(_itemConfig.ID)] = _targetTime.ToString();
            
            else if (!_saveSystem.Data.ShopItemsTimers.ContainsKey(Int32.Parse(_itemConfig.ID)) && _isLocked)
                _saveSystem.Data.ShopItemsTimers.Add(Int32.Parse(_itemConfig.ID), _targetTime.ToString());
            
            _saveSystem.SaveData();
            Destroy(gameObject);
        }

        private void SetDefaultParams()
        {
            _price = (int)_itemConfig.PriceValue;
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
            var currentTime = DateTime.Now;
            _targetTime = currentTime.AddMinutes(_itemConfig.CooldownInMinutes);
            if (_saveSystem.Data.ShopItemsTimers.ContainsKey(Int32.Parse(_itemConfig.ID)))
            {
                _targetTime = DateTime.Parse(_saveSystem.Data.ShopItemsTimers[Int32.Parse(_itemConfig.ID)]);
                CheckTime();
            }
            DoLock();
        }

        private void CheckTime()
        {
            var time = _targetTime - DateTime.Now;

            if (time.Seconds < 0)
            {
                _saveSystem.Data.ShopItemsTimers.Remove(Int32.Parse(_itemConfig.ID));
                _saveSystem.SaveData();
                SetDefaultParams();
            }
        }
    }
}