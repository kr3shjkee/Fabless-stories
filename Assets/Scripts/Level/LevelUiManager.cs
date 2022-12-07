using Common;
using GameUi;
using Signals.Ui;
using UnityEngine.SceneManagement;
using Zenject;

namespace Level
{
    public class LevelUiManager : BaseUiManager
    {
        private readonly LevelUiPanelsController _levelUiPanelsController;
        
        public LevelUiManager(SignalBus signalBus, SaveSystem saveSystem, LevelUiPanelsController levelUiPanelsController)
        {
            _signalBus = signalBus;
            _saveSystem = saveSystem;
            _levelUiPanelsController = levelUiPanelsController;
        }
        
        protected override void SubscribeSignals()
        {
            base.SubscribeSignals();
            
        }

        protected override void UnsubscribeSignals()
        {
            base.UnsubscribeSignals();
            
        }
        protected override void ShowHealthPanel()
        {
            _levelUiPanelsController.ShowHealthPanel();
        }

        protected override void ShowShopPanel()
        {
            _levelUiPanelsController.ShowShopPanel();
        }

        protected override void ShowOptionsPanel()
        {
            _levelUiPanelsController.ShowOptionsPanel();
        }

        protected override void ShowSoundOptionsPanel()
        {
            _levelUiPanelsController.ShowSoundOptionsPanel();
        }

        protected override void BackToPreviousScene()
        {
            SceneManager.LoadScene("GameScene");
        }

        protected override void CloseCurrentPanel(OnCloseCurrentPanelSignal signal)
        {
            _levelUiPanelsController.CloseCurrentPanel(signal._currentPanel);
        }
    }
}