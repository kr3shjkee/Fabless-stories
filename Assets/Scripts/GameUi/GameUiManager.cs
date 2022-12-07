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

        public GameUiManager(SignalBus signalBus, SaveSystem saveSystem, GameUiPanelsController gameUiPanelsController)
        {
            _signalBus = signalBus;
            _saveSystem = saveSystem;
            _gameUiPanelsController = gameUiPanelsController;
        }
        
        protected override void SubscribeSignals()
        {
            base.SubscribeSignals();
            _signalBus.Subscribe<ComingSoonSignal>(ShowComingSoonPanel);
        }

        protected override void UnsubscribeSignals()
        {
            base.UnsubscribeSignals();
            _signalBus.Unsubscribe<ComingSoonSignal>(ShowComingSoonPanel);
        }

        protected override void ShowHealthPanel()
        {
            _gameUiPanelsController.ShowHealthPanel();
        }

        protected override void ShowShopPanel()
        {
            _gameUiPanelsController.ShowShopPanel();
        }

        protected override void ShowOptionsPanel()
        {
            _gameUiPanelsController.ShowOptionsPanel();
        }

        protected override void ShowSoundOptionsPanel()
        {
            _gameUiPanelsController.ShowSoundOptionsPanel();
        }

        protected override void BackToPreviousScene()
        {
            SceneManager.LoadScene("LoadingScene");
        }

        protected override void CloseCurrentPanel(OnCloseCurrentPanelSignal signal)
        {
            _gameUiPanelsController.CloseCurrentPanel(signal._currentPanel);
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
    }
}