using System;
using Signals.Loading;
using Signals.Ui;
using Zenject;
using OnExitButtonClickSignal = Signals.Ui.OnExitButtonClickSignal;
using OnOptionsButtonClickSignal = Signals.Ui.OnOptionsButtonClickSignal;

namespace Common
{
    public abstract class BaseUiManager : IInitializable, IDisposable
    {
        protected SignalBus _signalBus;
        protected SaveSystem _saveSystem;
        protected SoundManager _soundManager;

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
            _signalBus.Subscribe<OnSoundOptionChangedSignal>(SaveSoundOptions);
            _signalBus.Subscribe<OnMusicOptionChangedSignal>(SaveMusicOptions);
            _signalBus.Subscribe<OnUpdateUiValuesSignal>(UpdateUiValues);
            _signalBus.Subscribe<OnShopPanelCloseSignal>(CloseShopPanel);
            _signalBus.Subscribe<OnUpdateGoldAfterPurchaseSignal>(UpdateGoldAfterPurchase);
        }

        protected virtual void UnsubscribeSignals()
        {
            _signalBus.Unsubscribe<OnOptionsButtonClickSignal>(ShowOptionsPanel);
            _signalBus.Unsubscribe<OnHealthButtonClickSignal>(ShowHealthPanel);
            _signalBus.Unsubscribe<OnSoundOptionsButtonClickSignal>(ShowSoundOptionsPanel);
            _signalBus.Unsubscribe<OnShopButtonClickSignal>(ShowShopPanel);
            _signalBus.Unsubscribe<OnExitButtonClickSignal>(BackToPreviousScene);
            _signalBus.Unsubscribe<OnCloseCurrentPanelSignal>(CloseCurrentPanel);
            _signalBus.Unsubscribe<OnSoundOptionChangedSignal>(SaveSoundOptions);
            _signalBus.Unsubscribe<OnMusicOptionChangedSignal>(SaveMusicOptions);
            _signalBus.Unsubscribe<OnUpdateUiValuesSignal>(UpdateUiValues);
            _signalBus.Unsubscribe<OnShopPanelCloseSignal>(CloseShopPanel);
            _signalBus.Unsubscribe<OnUpdateGoldAfterPurchaseSignal>(UpdateGoldAfterPurchase);
        }

        public abstract void UpdateUiValues();

        public abstract void ShowHealthPanel();
        
        public abstract void ShowShopPanel();

        public abstract void ShowOptionsPanel();

        public abstract void ShowSoundOptionsPanel();

        public abstract void BackToPreviousScene();
        
        public abstract void CloseShopPanel();

        protected abstract void CloseCurrentPanel(OnCloseCurrentPanelSignal signal);
        
        public abstract void UpdateGoldAfterPurchase();

        protected void SaveSoundOptions(OnSoundOptionChangedSignal signal)
        {
            _saveSystem.Data.IsSoundMute = signal.IsMute;
            _saveSystem.SaveData();
        }
        
        protected void SaveMusicOptions(OnMusicOptionChangedSignal signal)
        {
            _saveSystem.Data.IsMusicMute = signal.IsMute;
            _saveSystem.SaveData();
        }

        protected void LoadSoundOptions()
        {
            _soundManager.IsSoundMute(_saveSystem.Data.IsSoundMute);
            _soundManager.IsMusicMute(_saveSystem.Data.IsMusicMute);
        }
        
    }
}