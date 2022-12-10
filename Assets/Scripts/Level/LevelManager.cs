using System;
using System.Threading;
using Common;
using Configs;
using Cysharp.Threading.Tasks;
using LevelUi;
using Signals.Level;
using Signals.Ui;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Level
{
    public class LevelManager : IInitializable, IDisposable
    {
        private const int HEALTH_RESTORE_PRICE = 1000;
        private const int STEPS_RESTORE_PRICE = 1000;
        private const int BACKSTEPS_RESTORE_PRICE = 1000;
        private const float SHOW_HINT_TIME = 5f;
        
        private readonly SignalBus _signalBus;
        private readonly SaveSystem _saveSystem;
        private readonly LevelUiManager _levelUiManager;
        private readonly BoardController _boardController;
        private readonly LevelConfigs _levelConfigs;
        private readonly SoundManager _soundManager;
        
        private bool _isNeedToCancel;
        private int _currentSteps;
        private int _currentBackSteps;
        private int _currentHealth;
        private int _currentGold;
        private int _currentTargets;
        private LevelCfg _currentLevelConfig;
        

        public LevelManager(SignalBus signalBus, SaveSystem saveSystem, BoardController boardController, 
            LevelUiManager levelUiManager, LevelConfigs levelConfigs, SoundManager soundManager)
        {
            _signalBus = signalBus;
            _saveSystem = saveSystem;
            _boardController = boardController;
            _levelUiManager = levelUiManager;
            _levelConfigs = levelConfigs;
            _soundManager = soundManager;
        }
        
        public void Initialize()
        {
            SubscribeSignals();
            _saveSystem.LoadData();
            _currentLevelConfig = _levelConfigs.GetLevelConfigByNumber(_saveSystem.Data.CurrentLevelNumber);
            InitValues();
            CreateBoard();
            StartTimer();
        }

        private void InitValues()
        {
            ChangeSteps(_currentLevelConfig.StepsCount);
            ChangeHealth(_saveSystem.Data.HealthValue);
            ChangeBackSteps(_saveSystem.Data.CurrentBackStepsCount);
            ChangeGold(_saveSystem.Data.Gold);
            ChangeTargets(_currentLevelConfig.TargetCount);
        }

        private void ChangeGold(int gold)
        {
            _currentGold = gold;
            _saveSystem.Data.Gold = _currentGold;
            _saveSystem.SaveData();
            _signalBus.Fire(new OnGoldChangedSignal(_currentGold));
        }

        private void ChangeBackSteps(int backSteps)
        {
            _currentBackSteps = backSteps;
            _saveSystem.Data.CurrentBackStepsCount = _currentBackSteps;
            _saveSystem.SaveData();
            _signalBus.Fire(new OnBackStepsChangedSignal(_currentBackSteps));
        }

        private void ChangeHealth(int health)
        {
            _currentHealth = health;
            _saveSystem.Data.HealthValue = _currentHealth;
            _saveSystem.SaveData();
            _signalBus.Fire(new OnHealthChangedSignal(_currentHealth));
        }

        private void ChangeSteps(int steps)
        {
            _currentSteps = steps;
            _signalBus.Fire(new OnStepsChangedSignal(_currentSteps));
        }

        private void ChangeTargets(int value)
        {
            _currentTargets = value;
            _signalBus.Fire(new OnTargetsChangedSignal(_currentTargets));
        }

        private void CreateBoard()
        {
            _boardController.Initialize();
        }

        public void Dispose()
        {
            UnSubscribeSignals();
            _saveSystem.SaveData();
        }

        private void SubscribeSignals()
        {
            _signalBus.Subscribe<OnBoardMatchSignal>(Match);
            _signalBus.Subscribe<OnDoStepSignal>(DoStep);
            _signalBus.Subscribe<OnBackStepSignal>(BackStep);
            _signalBus.Subscribe<OnBackStepsRestoredSignal>(RestoreBackSteps);
            _signalBus.Subscribe<OnRestartSignal>(RestartLevel);
            _signalBus.Subscribe<OnHealthRestoreSignal>(RestoreHealth);
            _signalBus.Subscribe<OnStepsRestoredSignal>(RestoreSteps);
        }

        private void UnSubscribeSignals()
        {
            _signalBus.Unsubscribe<OnBoardMatchSignal>(Match);
            _signalBus.Unsubscribe<OnDoStepSignal>(DoStep);
            _signalBus.Unsubscribe<OnBackStepSignal>(BackStep);
            _signalBus.Unsubscribe<OnBackStepsRestoredSignal>(RestoreBackSteps);
            _signalBus.Unsubscribe<OnRestartSignal>(RestartLevel);
            _signalBus.Unsubscribe<OnHealthRestoreSignal>(RestoreHealth);
            _signalBus.Unsubscribe<OnStepsRestoredSignal>(RestoreSteps);
        }

        private void Match(OnBoardMatchSignal signal)
        {
            if (signal.Key == _currentLevelConfig.GetTarget())
            {
                _currentTargets -= signal.Count;
                if (_currentTargets<=0)
                    WinLevel();
                
                ChangeTargets(_currentTargets);
            }
        }

        private void WinLevel()
        {
            _saveSystem.Data.CurrentLevelNumber++;
            _saveSystem.Data.IsNeedToMove = true;
            _saveSystem.SaveData();
            _levelUiManager.ShowWinPanel();
            _soundManager.WinSoundPlay();
        }


        private void DoStep()
        {
            ChangeSteps(--_currentSteps);
            if (_currentSteps > 0)
                StartTimer();
            else
                LoseLevel();
        }

        private void LoseLevel()
        {
            _levelUiManager.ShowFailPanel();
        }

        private void RestartLevel()
        {
            _saveSystem.Data.HealthValue--;
            _saveSystem.SaveData();
            SceneManager.LoadScene("LevelScene");
        }
        
        private async void StartTimer()
        {
            var cts = new CancellationTokenSource();
            var _cts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token);
            
            if (_isNeedToCancel)
                _cts.Cancel();
            
            try
            {
                Debug.Log("Timer was started");
                _isNeedToCancel = true;
                await UniTask.Delay(TimeSpan.FromSeconds(SHOW_HINT_TIME), cancellationToken: _cts.Token);
                _isNeedToCancel = false;
                _signalBus.Fire<OnElementMatchShowSignal>();
                Debug.Log("Show hint");
            }
            catch (Exception e)
            {
                if (!(e is OperationCanceledException))
                {
                    Debug.Log(e.Message);
                    throw;
                }
                Debug.Log("Hint was canceled");
                _cts.Dispose();
                _isNeedToCancel = false;
            }
        }

        private void BackStep()
        {
            if (_saveSystem.Data.CurrentBackStepsCount > 0)
            {
                _saveSystem.Data.CurrentBackStepsCount--;
                ChangeSteps(++_currentSteps);
                ChangeBackSteps(--_currentBackSteps);
                _saveSystem.SaveData();
                _boardController.OnBackStep();
            }
            else
            {
                _levelUiManager.ShowBackStepsPanel();
            }
        }

        private void RestoreBackSteps()
        {
            _levelUiManager.HideBackStepsPanel();
            if (_currentGold>=BACKSTEPS_RESTORE_PRICE)
            {
                ChangeGold(_currentGold-BACKSTEPS_RESTORE_PRICE);
                _currentBackSteps = _saveSystem.Data.DEFAULT_BACKSTEPS_VALUE;
                ChangeBackSteps(_currentBackSteps);
            }
            else
            {
                _levelUiManager.ShowShopPanel();
            }
        }

        private void RestoreHealth()
        {
            if (_currentGold>=HEALTH_RESTORE_PRICE)
            {
                ChangeGold(_currentGold-HEALTH_RESTORE_PRICE);
                _currentHealth = _saveSystem.Data.DEFAULT_HEALTH_VALUE;
                ChangeHealth(_currentHealth);
            }
            else
            {
                _levelUiManager.ShowShopPanel();
            }
        }

        private void RestoreSteps()
        {
            if (_currentGold >= STEPS_RESTORE_PRICE)
            {
                ChangeGold(_currentGold-STEPS_RESTORE_PRICE);
                _currentSteps = 5;
                ChangeSteps(_currentSteps);
                _levelUiManager.HideFailPanel();
            }
            else
            {
                _levelUiManager.ShowShopPanel();
            }
        }
    }
}