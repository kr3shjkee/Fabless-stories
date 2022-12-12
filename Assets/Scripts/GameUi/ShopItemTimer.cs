using System;
using Common;
using Signals.Ui;

namespace GameUi
{
    public class ShopItemTimer : BaseTimer
    {
        private int _delayInMinutes;
        private int _key;

        protected override void Start()
        {
            base.Start();
            _signalBus.Subscribe<OnShopItemTimerStartSignal>(Init);
        }

        private void Init(OnShopItemTimerStartSignal signal)
        {
            _delayInMinutes = signal.TimerValue;
            _key = signal.Key;
            var currentTime = DateTime.Now;
            _targetTime = currentTime.AddMinutes(_delayInMinutes);
        }

        protected override void Update()
        {
            
        }

        protected override void OnDestroy()
        {
            _signalBus.Unsubscribe<OnShopItemTimerStartSignal>(Init);
        }

        protected override void CheckTime()
        {
            var time = _targetTime - DateTime.Now;

            if (time.Seconds < 0)
            {
                timerText.gameObject.SetActive(false);
                _saveSystem.Data.ShopItemsTimers.ContainsKey(_key);
                _saveSystem.SaveData();
                _signalBus.Fire<OnUpdateUiValuesSignal>();
            }
        }
    }
}