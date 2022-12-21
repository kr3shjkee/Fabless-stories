using Common;
using Level;
using LevelUi;
using Signals.DebugMenu;
using Zenject;

namespace DebugMenu
{
    public class LevelDebugMenu : BaseDebugManager
    {
        private LevelUiManager _levelUiManager;
        private LevelManager _levelManager;

        [Inject]
        public void Construct(SignalBus signalBus, LevelUiManager levelUiManager, SaveSystem saveSystem, 
            LevelManager levelManager)
        {
            _signalBus = signalBus;
            _levelUiManager = levelUiManager;
            _saveSystem = saveSystem;
            _levelManager = levelManager;
        }
        
        protected override void Start()
        {
            base.Start();
            _signalBus.Subscribe<OnLevelCompleteDebugSignal>(LevelComplete);
            _signalBus.Subscribe<OnSetLastStepSignal>(SetLastStep);
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            _signalBus.Unsubscribe<OnLevelCompleteDebugSignal>(LevelComplete);
            _signalBus.Unsubscribe<OnSetLastStepSignal>(SetLastStep);
        }
        
        protected override void HealthLost()
        {
            base.HealthLost();
            _levelUiManager.UpdateHealthValue();
            _saveSystem.SaveData();
        }

        protected override void HealthFull()
        {
            base.HealthFull();
            _levelUiManager.UpdateHealthValue();
            _saveSystem.SaveData();
        }

        protected override void GoldLost()
        {
            base.GoldLost();
            _levelUiManager.UpdateGoldAfterPurchase();
            _saveSystem.SaveData();
        }

        protected override void GoldTenK()
        {
            base.GoldTenK();
            _levelUiManager.UpdateGoldAfterPurchase();
            _saveSystem.SaveData();
        }

        private void LevelComplete()
        {
            _levelManager.WinLevel();
            CloseDebug();
        }

        private void SetLastStep()
        {
            _levelManager.ChangeSteps(1);
            CloseDebug();
        }
    }
}