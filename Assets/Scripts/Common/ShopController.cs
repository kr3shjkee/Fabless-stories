using System;
using System.Collections.Generic;
using Configs;
using Game;
using GameUi;
using Signals.Ads;
using Signals.Analytics;
using Signals.Ui;
using UnityEngine;
using Zenject;

namespace Common
{
    public class ShopController : IInitializable, IDisposable
    {
        private const string ITEMS_PARENT_TAG = "ItemsParent";
        
        private ShopItem.Factory _factory;
        private SignalBus _signalBus;
        private ShopItemConfig _shopItemConfig;
        private GameObject _parentForItems;
        private SaveSystem _saveSystem;

        private List<ShopItem> _items = new List<ShopItem>();

        [Inject]
        public ShopController(SignalBus signalBus, ShopItemConfig shopItemConfig, ShopItem.Factory factory, 
         SaveSystem saveSystem)
        {
            _signalBus = signalBus;
            _factory = factory;
            _shopItemConfig = shopItemConfig;
            _saveSystem = saveSystem;
        }
        
        
        public void Initialize()
        {
            _signalBus.Subscribe<OnShopPanelsOpenSignal>(CreateShopItems);
            _signalBus.Subscribe<OnShopElementClickSignal>(ShowClickedItem);
            _signalBus.Subscribe<OnShopItemBuyClick>(ShowRewardAd);
            _signalBus.Subscribe<OnShopPanelCloseSignal>(DestroyItems);
            _signalBus.Subscribe<EndRewardedAdSignal>(BuyItem);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<OnShopPanelsOpenSignal>(CreateShopItems);
            _signalBus.Unsubscribe<OnShopElementClickSignal>(ShowClickedItem);
            _signalBus.Unsubscribe<OnShopItemBuyClick>(ShowRewardAd);
            _signalBus.Unsubscribe<OnShopPanelCloseSignal>(DestroyItems);
            _signalBus.Unsubscribe<EndRewardedAdSignal>(BuyItem);
        }
        

        private void CreateShopItems()
        {
            _parentForItems = GameObject.FindGameObjectWithTag(ITEMS_PARENT_TAG);
            for (int i = 0; i < _shopItemConfig.Items.Length; i++)
            {
                var itemGameObject = _factory.Create(_shopItemConfig.Items[i]);
                itemGameObject.gameObject.transform.SetParent(_parentForItems.transform);
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
            _saveSystem.Data.Gold += selectedItem.Price;
            _saveSystem.SaveData();
            _signalBus.Fire<OnUpdateGoldAfterPurchaseSignal>();
            _signalBus.Fire(new OnBuyAdRewardItemsSignal(selectedItem.Id,selectedItem.Price));
            selectedItem.SetSelected(false);
            selectedItem.StartTimer();
            
        }

        private void ShowRewardAd()
        {
            var selectedItem = _items.Find(item => item.IsSelected);
            if (selectedItem != null)
            {
                _signalBus.Fire<OnShowRewardedAdSignal>();
            }
        }

        private void DestroyItems()
        {
            foreach (var item in _items)
            {
                item.DestroyItem();
            }
            _items.Clear();
        }
    }
}