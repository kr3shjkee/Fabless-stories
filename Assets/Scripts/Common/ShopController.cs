using System;
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

        private List<ShopItem> _items;

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
            _signalBus.Subscribe<OnAddShopItemToListSignal>(TakeShopItem);
            
            for (int i = 0; i < _shopItemConfig.Items.Length; i++)
            {
                var item = _factory.Create(_shopItemConfig.Items[i]).gameObject;
                item.transform.SetParent(_gameUiPanelsController.gameObject.transform);
            }
            _signalBus.Fire<OnInitShopItemsSignal>();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<OnAddShopItemToListSignal>(TakeShopItem);
        }

        private void TakeShopItem(OnAddShopItemToListSignal signal)
        {
            _items.Add(signal.Item);
        }
    }
}