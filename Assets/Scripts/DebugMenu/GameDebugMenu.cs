using Common;
using Game;
using GameUi;
using Signals.DebugMenu;
using Zenject;

namespace DebugMenu
{
    public class GameDebugMenu : BaseDebugManager
    {
        private GameUiManager _gameUiManager;
        private GameManager _gameManager;

        [Inject]
        public void Construct(SignalBus signalBus, GameUiManager gameUiManager, SaveSystem saveSystem, 
            GameManager gameManager)
        {
            _signalBus = signalBus;
            _gameUiManager = gameUiManager;
            _saveSystem = saveSystem;
            _gameManager = gameManager;
        }

        protected override void Start()
        {
            base.Start();
            _signalBus.Subscribe<OnMoveToNextLevelSignal>(NextLevel);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _signalBus.Unsubscribe<OnMoveToNextLevelSignal>(NextLevel);
        }

        protected override void HealthLost()
        {
            base.HealthLost();
            _gameUiManager.UpdateUiValues();
        }

        protected override void HealthFull()
        {
            base.HealthFull();
            _gameUiManager.UpdateUiValues();
        }

        protected override void GoldLost()
        {
            base.GoldLost();
            _gameUiManager.UpdateUiValues();
        }

        protected override void GoldTenK()
        {
            base.GoldTenK();
            _gameUiManager.UpdateUiValues();
        }

        private void NextLevel()
        {
            if (_saveSystem.Data.CurrentLevelNumber + 1 < 6)
            {
                _saveSystem.Data.CurrentLevelNumber++;
                _saveSystem.SaveData();
                _gameManager.NextLevel();
            }
            
            CloseDebug();
        }
        
    }
}