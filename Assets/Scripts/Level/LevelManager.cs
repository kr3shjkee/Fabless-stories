using System;
using System.Threading;
using Common;
using Configs;
using Cysharp.Threading.Tasks;
using Signals.Level;
using UnityEngine;
using Zenject;

namespace Level
{
    public class LevelManager : IInitializable, IDisposable
    {
        private const int SCORE_FOR_ELEMENTS = 1;
        private const float WAIT_TIME = 5f;
        
        private readonly SignalBus _signalBus;
        private readonly SaveSystem _saveSystem;
        private readonly LevelUiPanelsController _levelUiPanelsController;
        private readonly BoardController _boardController;
        private readonly LevelConfigs _levelConfigs;
        
        private bool _isNeedToCancel;
        private int _currentSteps;
        private LevelCfg _currentLevelConfig;
        

        public LevelManager(SignalBus signalBus, SaveSystem saveSystem, BoardController boardController, 
            LevelUiPanelsController levelUiPanelsController, LevelConfigs levelConfigs)
        {
            _signalBus = signalBus;
            _saveSystem = saveSystem;
            _boardController = boardController;
            _levelUiPanelsController = levelUiPanelsController;
            _levelConfigs = levelConfigs;
        }
        
        public void Initialize()
        {
            SubscribeSignals();
            _saveSystem.LoadData();
            _currentLevelConfig = _levelConfigs.GetLevelConfigByNumber(_saveSystem.Data.CurrentLevelNumber);
            InitializeUiValues();
            CreateBoard();
            StartTimer();
        }

        private void InitializeUiValues()
        {
            _levelUiPanelsController.UpdateStepsValue(_currentLevelConfig.StepsCount);
            _levelUiPanelsController.UpdateTargetValue(_currentLevelConfig.TargetCount);
            _levelUiPanelsController.UpdateTargetIcon(_levelConfigs.GetTargetByKey(_currentLevelConfig).Sprite);
            _levelUiPanelsController.UpdateBackStepsValue(_saveSystem.Data.CurrentBackStepsCount);
            _levelUiPanelsController.UpdateHealthValue(_saveSystem.Data.HealthValue);
            _levelUiPanelsController.UpdateGoldValue(_saveSystem.Data.Gold);
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
        }

        private void UnSubscribeSignals()
        {
            _signalBus.Unsubscribe<OnBoardMatchSignal>(Match);
            _signalBus.Unsubscribe<OnDoStepSignal>(DoStep);
        }

        private void Match(OnBoardMatchSignal signal)
        {
            
        }
        

        private void UpdateSteps()
        {
            _currentSteps--;
            _levelUiPanelsController.UpdateStepsValue(_currentSteps);
        }

        private void DoStep()
        {
            UpdateSteps();
            if (_currentSteps > 0)
                StartTimer();
            else
                LoseLevel();
        }

        private void LoseLevel()
        {
            
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
                await UniTask.Delay(TimeSpan.FromSeconds(WAIT_TIME), cancellationToken: _cts.Token);
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
    }
}