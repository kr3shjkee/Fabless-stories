using System;
using Common;
using Signals.Loading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Application = UnityEngine.Device.Application;

namespace Loading
{
    public class LoadingUiManager : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly SaveSystem _saveSystem;
        private readonly UiPanelsController _uiPanelsController;
        private SoundManager _soundManager;

        public LoadingUiManager(SignalBus signalBus, SaveSystem saveSystem, UiPanelsController uiPanelsController, SoundManager soundManager)
        {
            _signalBus = signalBus;
            _saveSystem = saveSystem;
            _uiPanelsController = uiPanelsController;
            _soundManager = soundManager;
        }
        
        public void Initialize()
        {
            SubscribeSignals();
            if (_saveSystem.CheckOnNewProfile())
            {
                ShowTermOfUse();
            }
            else
            {
                _saveSystem.LoadData();
                LoadSoundOptions();
                ShowMainMenu();
            }
        }

        public void Dispose()
        {
            UnSubscribeSignals();
        }

        private void SubscribeSignals()
        {
            _signalBus.Subscribe<OnAgreeButtonClickSignal>(ShowMainMenu);
            _signalBus.Subscribe<OnStartButtonClickSignal>(StartGameScene);
            _signalBus.Subscribe<OnBackButtonClickSignal>(ShowMainMenu);
            _signalBus.Subscribe<OnExitButtonClickSignal>(ExitApplication);
            _signalBus.Subscribe<OnOptionsButtonClickSignal>(ShowOptionMenu);
            _signalBus.Subscribe<OnSoundOptionChangedSignal>(SaveSoundOptions);
            _signalBus.Subscribe<OnMusicOptionChangedSignal>(SaveMusicOptions);
        }

        private void UnSubscribeSignals()
        {
            _signalBus.Unsubscribe<OnAgreeButtonClickSignal>(ShowMainMenu);
            _signalBus.Unsubscribe<OnStartButtonClickSignal>(StartGameScene);
            _signalBus.Unsubscribe<OnBackButtonClickSignal>(ShowMainMenu);
            _signalBus.Unsubscribe<OnExitButtonClickSignal>(ExitApplication);
            _signalBus.Unsubscribe<OnOptionsButtonClickSignal>(ShowOptionMenu);
            _signalBus.Unsubscribe<OnSoundOptionChangedSignal>(SaveSoundOptions);
            _signalBus.Unsubscribe<OnMusicOptionChangedSignal>(SaveMusicOptions);
        }

        private void ShowTermOfUse()
        {
            _uiPanelsController.ShowTermOfUsePanel();
        }
        private void ShowMainMenu()
        {
            _uiPanelsController.ShowMainMenuPanel();
        }
        

        private void StartGameScene()
        {
            SceneManager.LoadScene("GameScene");
        }

        private void ShowOptionMenu()
        {
            _uiPanelsController.ShowOptionsPanel();
        }

        private void ExitApplication()
        {
            _saveSystem.SaveData();
            Application.Quit();
        }

        private void SaveSoundOptions(OnSoundOptionChangedSignal signal)
        {
            _saveSystem.Data.IsSoundMute = signal.IsMute;
            _saveSystem.SaveData();
        }
        
        private void SaveMusicOptions(OnMusicOptionChangedSignal signal)
        {
            _saveSystem.Data.IsMusicMute = signal.IsMute;
            _saveSystem.SaveData();
        }

        private void LoadSoundOptions()
        {
            _soundManager.IsSoundMute(_saveSystem.Data.IsSoundMute);
            _soundManager.IsMusicMute(_saveSystem.Data.IsMusicMute);
        }
    }
}