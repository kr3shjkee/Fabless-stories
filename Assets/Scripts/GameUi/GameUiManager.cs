using Common;
using Signals.Game;
using Signals.Ui;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameUi
{
    public class GameUiManager : BaseUiManager
    {
        private readonly GameUiPanelsController _gameUiPanelsController;

        public GameUiManager(SignalBus signalBus, SaveSystem saveSystem, GameUiPanelsController gameUiPanelsController,
            SoundManager soundManager)
        {
            _signalBus = signalBus;
            _saveSystem = saveSystem;
            _gameUiPanelsController = gameUiPanelsController;
            _soundManager = soundManager;
        }

        public override void Initialize()
        {
            _saveSystem.LoadData();
            LoadSoundOptions();
            SubscribeSignals();
        }

        public override void Dispose()
        {
            UnsubscribeSignals();
        }

        protected override void SubscribeSignals()
        {
            base.SubscribeSignals();
            _signalBus.Subscribe<ComingSoonSignal>(ShowComingSoonPanel);
            _signalBus.Subscribe<OnPlayLevelButtonClickSignal>(GoToLevel);
        }

        protected override void UnsubscribeSignals()
        {
            base.UnsubscribeSignals();
            _signalBus.Unsubscribe<ComingSoonSignal>(ShowComingSoonPanel);
            _signalBus.Unsubscribe<OnPlayLevelButtonClickSignal>(GoToLevel);
        }

        public override void UpdateUiValues()
        {
            _gameUiPanelsController.UpdateGoldValue(_saveSystem.Data.Gold);
            _gameUiPanelsController.UpdateHealthValue(_saveSystem.Data.HealthValue);
        }

        public override void ShowHealthPanel()
        {
            _gameUiPanelsController.ShowHealthPanel();
        }

        public override void ShowShopPanel()
        {
            _gameUiPanelsController.ShowShopPanel();
            _signalBus.Fire<OnShopPanelsOpenSignal>();
        }

        public override void ShowOptionsPanel()
        {
            _gameUiPanelsController.ShowOptionsPanel();
        }

        public override void ShowSoundOptionsPanel()
        {
            _gameUiPanelsController.ShowSoundOptionsPanel();
        }

        public override void BackToPreviousScene()
        {
            _saveSystem.SaveData();
            SceneManager.LoadScene("LoadingScene");
        }

        public override void CloseShopPanel()
        {
            _gameUiPanelsController.CloseShopPanel();
        }

        protected override void CloseCurrentPanel(OnCloseCurrentPanelSignal signal)
        {
            _gameUiPanelsController.CloseCurrentPanel(signal._currentPanel);
        }

        public override void UpdateGoldAfterPurchase()
        {
            _gameUiPanelsController.UpdateGoldValue(_saveSystem.Data.Gold);
        }

        private void ShowComingSoonPanel()
        {
            _gameUiPanelsController.ShowComingSoonPanel();
        }
        
        public void ShowGameUi()
        {
            _gameUiPanelsController.OpenUi();
        }
        
        public void HideGameUi()
        {
            _gameUiPanelsController.CloseUi();
        }

        private void GoToLevel()
        {
            if (_saveSystem.Data.HealthValue==0)
            {
                ShowHealthPanel();
            }
            else
            {
                _saveSystem.SaveData();
                SceneManager.LoadScene("LevelScene");
            }
           
        }
        
    }
}