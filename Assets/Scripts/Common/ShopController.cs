﻿using System;
using System.Collections.Generic;
using Configs;
using GameUi;
using Signals.Ui;
using Zenject;

namespace Common
{
    public class ShopController : IInitializable, IDisposable
    {
        private ShopItem.Factory _factory;
        private SignalBus _signalBus;
        private ShopItemConfig _shopItemConfig;
        private GameUiPanelsController _gameUiPanelsController;

        private List<ShopItem> _items = new List<ShopItem>();

        [Inject]
        public ShopController(SignalBus signalBus, ShopItemConfig shopItemConfig, ShopItem.Factory factory, GameUiPanelsController gameUiPanelsController)
        {
            _signalBus = signalBus;
            _factory = factory;
            _shopItemConfig = shopItemConfig;
            _gameUiPanelsController = gameUiPanelsController;
        }
        
        
        public void Initialize()
        {
            _signalBus.Subscribe<OnShopPanelsOpenSignal>(CreateShopItems);
            _signalBus.Subscribe<OnShopElementClickSignal>(ShowClickedItem);
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
                itemGameObject.transform.SetParent(_gameUiPanelsController.gameObject.transform);
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
    }
}