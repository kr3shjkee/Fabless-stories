using System;
using Signals.Ui;
using Zenject;

namespace Common
{
    public abstract class BaseUiManager : IInitializable, IDisposable
    {
        protected SignalBus _signalBus;
        protected SaveSystem _saveSystem;

        public abstract void Initialize();

        public abstract void Dispose();

        protected virtual void SubscribeSignals()
        {
            _signalBus.Subscribe<OnOptionsButtonClickSignal>(ShowOptionsPanel);
            _signalBus.Subscribe<OnHealthButtonClickSignal>(ShowHealthPanel);
            _signalBus.Subscribe<OnSoundOptionsButtonClickSignal>(ShowSoundOptionsPanel);
            _signalBus.Subscribe<OnShopButtonClickSignal>(ShowShopPanel);
            _signalBus.Subscribe<OnExitButtonClickSignal>(BackToPreviousScene);
            _signalBus.Subscribe<OnCloseCurrentPanelSignal>(CloseCurrentPanel);
        }

        protected virtual void UnsubscribeSignals()
        {
            _signalBus.Unsubscribe<OnOptionsButtonClickSignal>(ShowOptionsPanel);
            _signalBus.Unsubscribe<OnHealthButtonClickSignal>(ShowHealthPanel);
            _signalBus.Unsubscribe<OnSoundOptionsButtonClickSignal>(ShowSoundOptionsPanel);
            _signalBus.Unsubscribe<OnShopButtonClickSignal>(ShowShopPanel);
            _signalBus.Unsubscribe<OnExitButtonClickSignal>(BackToPreviousScene);
            _signalBus.Unsubscribe<OnCloseCurrentPanelSignal>(CloseCurrentPanel);
        }

        public abstract void UpdateUiValues();

        public abstract void ShowHealthPanel();
        
        public abstract void ShowShopPanel();

        public abstract void ShowOptionsPanel();

        public abstract void ShowSoundOptionsPanel();

        public abstract void BackToPreviousScene();

        protected abstract void CloseCurrentPanel(OnCloseCurrentPanelSignal signal);
    }
}