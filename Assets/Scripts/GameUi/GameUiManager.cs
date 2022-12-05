using System;
using Common;
using Signals.Game;
using Signals.Ui;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameUi
{
    public class GameUiManager : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly SaveSystem _saveSystem;
        private readonly GameUiPanelsController _gameUiPanelsController;

        public GameUiManager(SignalBus signalBus, SaveSystem saveSystem, GameUiPanelsController gameUiPanelsController)
        {
            _signalBus = signalBus;
            _saveSystem = saveSystem;
            _gameUiPanelsController = gameUiPanelsController;
        }
        
        public void Initialize()
        {
            SubscribeSignals();
        }

        public void Dispose()
        {
            UnsubscribeSignals();
        }

        private void SubscribeSignals()
        {
            _signalBus.Subscribe<OnOptionsButtonClickSignal>(ShowOptionsPanel);
            _signalBus.Subscribe<OnHealthButtonClickSignal>(ShowHealthPanel);
            _signalBus.Subscribe<OnSoundOptionsButtonClickSignal>(ShowSoundOptionsPanel);
            _signalBus.Subscribe<OnShopButtonClickSignal>(ShowShopPanel);
            _signalBus.Subscribe<OnExitButtonClickSignal>(BackToLoadingScene);
            _signalBus.Subscribe<OnCloseCurrentPanelSignal>(CloseCurrentPanel);
            _signalBus.Subscribe<ComingSoonSignal>(ShowComingSoonPanel);
        }

        private void UnsubscribeSignals()
        {
            _signalBus.Unsubscribe<OnOptionsButtonClickSignal>(ShowOptionsPanel);
            _signalBus.Unsubscribe<OnHealthButtonClickSignal>(ShowHealthPanel);
            _signalBus.Unsubscribe<OnSoundOptionsButtonClickSignal>(ShowSoundOptionsPanel);
            _signalBus.Unsubscribe<OnShopButtonClickSignal>(ShowShopPanel);
            _signalBus.Unsubscribe<OnExitButtonClickSignal>(BackToLoadingScene);
            _signalBus.Unsubscribe<OnCloseCurrentPanelSignal>(CloseCurrentPanel);
            _signalBus.Unsubscribe<ComingSoonSignal>(ShowComingSoonPanel);
        }

        private void ShowHealthPanel()
        {
            _gameUiPanelsController.ShowHealthPanel();
        }

        private void ShowShopPanel()
        {
            _gameUiPanelsController.ShowShopPanel();
        }

        private void ShowOptionsPanel()
        {
            _gameUiPanelsController.ShowOptionsPanel();
        }

        private void ShowSoundOptionsPanel()
        {
            _gameUiPanelsController.ShowSoundOptionsPanel();
        }

        private void ShowComingSoonPanel()
        {
            _gameUiPanelsController.ShowComingSoonPanel();
        }

        private void BackToLoadingScene()
        {
            SceneManager.LoadScene("LoadingScene");
        }

        private void CloseCurrentPanel(OnCloseCurrentPanelSignal signal)
        {
            _gameUiPanelsController.CloseCurrentPanel(signal._currentPanel);
        }

        public void ShowGameUi()
        {
            _gameUiPanelsController.OpenUi();
        }

        public void HideGameUi()
        {
            _gameUiPanelsController.CloseUi();
        }
    }
}