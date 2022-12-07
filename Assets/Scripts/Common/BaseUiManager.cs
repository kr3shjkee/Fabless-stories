using System;
using Signals.Ui;
using Zenject;

namespace Common
{
    public abstract class BaseUiManager : IInitializable, IDisposable
    {
        protected SignalBus _signalBus;
        protected SaveSystem _saveSystem;
        
        public virtual void Initialize()
        {
            SubscribeSignals();
        }

        public virtual void Dispose()
        {
            UnsubscribeSignals();
        }
        
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

        protected abstract void ShowHealthPanel();
        
        protected abstract void ShowShopPanel();

        protected abstract void ShowOptionsPanel();

        protected abstract void ShowSoundOptionsPanel();

        protected abstract void BackToPreviousScene();

        protected abstract void CloseCurrentPanel(OnCloseCurrentPanelSignal signal);
    }
}