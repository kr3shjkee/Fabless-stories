﻿using System;
using Loading;
using Signals.Ui;
using UnityEngine.SceneManagement;
using Zenject;

namespace Ui
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
        }

        private void UnsubscribeSignals()
        {
            _signalBus.Unsubscribe<OnOptionsButtonClickSignal>(ShowOptionsPanel);
            _signalBus.Unsubscribe<OnHealthButtonClickSignal>(ShowHealthPanel);
            _signalBus.Unsubscribe<OnSoundOptionsButtonClickSignal>(ShowSoundOptionsPanel);
            _signalBus.Unsubscribe<OnShopButtonClickSignal>(ShowShopPanel);
            _signalBus.Unsubscribe<OnExitButtonClickSignal>(BackToLoadingScene);
            _signalBus.Unsubscribe<OnCloseCurrentPanelSignal>(CloseCurrentPanel);
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

        private void BackToLoadingScene()
        {
            SceneManager.LoadScene("LoadingScene");
        }

        private void CloseCurrentPanel(OnCloseCurrentPanelSignal signal)
        {
            _gameUiPanelsController.CloseCurrentPanel(signal._currentPanel);
        }
    }
}