using System;
using Common;
using Configs;
using Cysharp.Threading.Tasks;
using Level;
using Signals.Level;
using Signals.Ui;
using UnityEngine.SceneManagement;
using Zenject;

namespace LevelUi
{
    public class LevelUiManager : BaseUiManager
    {
        private const float WIN_TIME = 5f;
        
        private readonly LevelUiPanelsController _levelUiPanelsController;
        private readonly LevelConfigs _levelConfigs;

        private LevelCfg _currentLevelConfig;
        
        public LevelUiManager(SignalBus signalBus, SaveSystem saveSystem, LevelUiPanelsController levelUiPanelsController, 
            LevelConfigs levelConfigs, SoundManager soundManager)
        {
            _signalBus = signalBus;
            _saveSystem = saveSystem;
            _levelUiPanelsController = levelUiPanelsController;
            _levelConfigs = levelConfigs;
            _soundManager = soundManager;
        }


        public override void Initialize()
        {
            _saveSystem.LoadData();
            LoadSoundOptions();
            SubscribeSignals();
            UpdateUiValues();
        }

        public override void Dispose()
        {
            UnsubscribeSignals();
        }

        protected override void SubscribeSignals()
        {
            base.SubscribeSignals();
            _signalBus.Subscribe<OnHealthChangedSignal>(UpdateHealth);
            _signalBus.Subscribe<OnStepsChangedSignal>(UpdateSteps);
            _signalBus.Subscribe<OnBackStepsChangedSignal>(UpdateBackSteps);
            _signalBus.Subscribe<OnGoldChangedSignal>(UpdateGold);
            _signalBus.Subscribe<OnTargetsChangedSignal>(UpdateTargets);
        }

        protected override void UnsubscribeSignals()
        {
            base.UnsubscribeSignals();
            _signalBus.Unsubscribe<OnHealthChangedSignal>(UpdateHealth);
            _signalBus.Unsubscribe<OnStepsChangedSignal>(UpdateSteps);
            _signalBus.Unsubscribe<OnBackStepsChangedSignal>(UpdateBackSteps);
            _signalBus.Unsubscribe<OnGoldChangedSignal>(UpdateGold);
            _signalBus.Unsubscribe<OnTargetsChangedSignal>(UpdateTargets);
        }

        public override void UpdateUiValues()
        {
            _currentLevelConfig = _levelConfigs.GetLevelConfigByNumber(_saveSystem.Data.CurrentLevelNumber);
            _levelUiPanelsController.UpdateTargetIcon(_levelConfigs.GetTargetByKey(_currentLevelConfig).Sprite);
        }

        public override void ShowHealthPanel()
        {
            _levelUiPanelsController.ShowHealthPanel();
        }

        public override void ShowShopPanel()
        {
            _levelUiPanelsController.ShowShopPanel();
            _signalBus.Fire<OnShopPanelsOpenSignal>();
        }

        public override void ShowOptionsPanel()
        {
            _levelUiPanelsController.ShowOptionsPanel();
        }

        public override void ShowSoundOptionsPanel()
        {
            _levelUiPanelsController.ShowSoundOptionsPanel();
        }

        public override void BackToPreviousScene()
        {
            _signalBus.Fire<OnLeaveSceneSignal>();
            _saveSystem.SaveData();
            _levelUiPanelsController.HideFailPanel();
            SceneManager.LoadScene("GameScene");
        }

        public override void CloseShopPanel()
        {
            _levelUiPanelsController.CloseShopPanel();
        }

        protected override void CloseCurrentPanel(OnCloseCurrentPanelSignal signal)
        {
            _levelUiPanelsController.CloseCurrentPanel(signal._currentPanel);
        }

        protected override void UpdateGoldAfterPurchase()
        {
            _levelUiPanelsController.UpdateGoldValue(_saveSystem.Data.Gold);
        }

        public void ShowFailPanel()
        {
            _levelUiPanelsController.ShowFailPanel();
        }

        public void HideFailPanel()
        {
            _levelUiPanelsController.HideFailPanel();
        }

        public void ShowBackStepsPanel()
        {
            _levelUiPanelsController.ShowBackStepsRestorePanel();
        }
        
        public void HideBackStepsPanel()
        {
            _levelUiPanelsController.HideBackStepsRestorePanel();
        }

        private void UpdateHealth(OnHealthChangedSignal signal)
        {
            _levelUiPanelsController.UpdateHealthValue(signal.Value);
        }
        
        private void UpdateSteps(OnStepsChangedSignal signal)
        {
            _levelUiPanelsController.UpdateStepsValue(signal.Value);
        }
        
        private void UpdateBackSteps(OnBackStepsChangedSignal signal)
        {
            _levelUiPanelsController.UpdateBackStepsValue(signal.Value);
        }

        private void UpdateGold(OnGoldChangedSignal signal)
        {
            _levelUiPanelsController.UpdateGoldValue(signal.Value);
        }

        private void UpdateTargets(OnTargetsChangedSignal signal)
        {
            _levelUiPanelsController.UpdateTargetValue(signal.Value);
        }

        public async void ShowWinPanel()
        {
            _levelUiPanelsController.ShowWinPanel();
            await UniTask.Delay(TimeSpan.FromSeconds(WIN_TIME));
            SceneManager.LoadScene("GameScene");
        }
    }
}