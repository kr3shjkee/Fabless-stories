using System;
using System.Collections.Generic;
using Configs;
using GameUi;
using Signals.Ui;
using UnityEngine;
using Zenject;

namespace Common
{
    public class ShopController : IInitializable, IDisposable, ITickable
    {
        private ShopItem.Factory _factory;
        private SignalBus _signalBus;
        private ShopItemConfig _shopItemConfig;
        private GameObject _parentForItems;
        private SaveSystem _saveSystem;
        private GameUiManager _gameUiManager;

        private List<ShopItem> _items;

        [Inject]
        public ShopController(SignalBus signalBus, ShopItemConfig shopItemConfig, ShopItem.Factory factory, 
            GameUiPanelsController gameUiPanelsController, SaveSystem saveSystem, GameUiManager gameUiManager)
        {
            _signalBus = signalBus;
            _factory = factory;
            _shopItemConfig = shopItemConfig;
            _parentForItems = gameUiPanelsController.gameObject;
            _saveSystem = saveSystem;
            _gameUiManager = gameUiManager;
        }
        
        
        public void Initialize()
        {
            _signalBus.Subscribe<OnShopPanelsOpenSignal>(CreateShopItems);
            _signalBus.Subscribe<OnShopElementClickSignal>(ShowClickedItem);
            _signalBus.Subscribe<OnShopItemBuyClick>(BuyItem);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<OnShopPanelsOpenSignal>(CreateShopItems);
            _signalBus.Unsubscribe<OnShopElementClickSignal>(ShowClickedItem);
        }
        

        private void CreateShopItems()
        {
            foreach (var element in _items)
            {
                element.DestroyItem();
            }

            _items.Clear();
            
            for (int i = 0; i < _shopItemConfig.Items.Length; i++)
            {
                var itemGameObject = _factory.Create(_shopItemConfig.Items[i]).gameObject;
                itemGameObject.transform.SetParent(_parentForItems.transform);
                var item = itemGameObject.GetComponent<ShopItem>();
                _items.Add(item);
            }
            _signalBus.Fire<OnInitShopItemsSignal>();
        }

        private void ShowClickedItem(OnShopElementClickSignal signal)
        {
            foreach (var item in _items)
            {
                item.SetSelected(false);
            }

            var clickedItem = signal.Item;
            clickedItem.SetSelected(true);
        }

        private void BuyItem()
        {
            var selectedItem = _items.Find(item => item.IsSelected);
            if (selectedItem != null)
            {
                _saveSystem.Data.Gold += selectedItem.Price;
                _saveSystem.SaveData();
                _gameUiManager.UpdateUiValues();
                selectedItem.SetSelected(false);
                _signalBus.Fire(new OnShopItemTimerStartSignal(Int32.Parse(selectedItem._itemConfig.ID), (int)selectedItem._itemConfig.CooldownInMinutes));
            }
        }

        public void Tick()
        {
            
        }
    }
}