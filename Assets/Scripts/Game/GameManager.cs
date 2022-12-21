using System;
using System.Linq;
using Common;
using Configs;
using GameUi;
using LevelUi;
using Signals.Ads;
using Signals.Analytics;
using Signals.Game;
using Signals.Ui;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameManager : IInitializable, IDisposable
    {
        private const int HEALTH_RESTORE_PRICE = 1000;
        
        private readonly SignalBus _signalBus;
        private readonly SaveSystem _saveSystem;
        private readonly GameUiManager _gameUiManager;
        private readonly ChapterDialog.Factory _chapterDialogFactory;
        private readonly ChapterDialogConfig[] _chapterDialogConfigs;
        private readonly PlayerController _player;
        private readonly ChapterMapController _chapterMapController;

        private ChapterDialogConfig _currentChapterDialogConfigs;
        private int _dialogIndex;
        private ChapterDialog _currentDialog;

        private DialogConfig[] _dialogConfigs;

        public GameManager(SignalBus signalBus, SaveSystem saveSystem, GameUiManager gameUiManager, 
            ChapterDialog.Factory chapterDialogFactory, ChapterConfig chapterConfig,
            PlayerController player, ChapterMapController chapterMapController)
        {
            _signalBus = signalBus;
            _saveSystem = saveSystem;
            _gameUiManager = gameUiManager;
            _chapterDialogFactory = chapterDialogFactory;
            _chapterDialogConfigs = chapterConfig.DialogConfigs;
            _player = player;
            _chapterMapController = chapterMapController;
        }
        
        public void Initialize()
        {
            SubscribeSignals();
            _gameUiManager.UpdateUiValues();
            _signalBus.Fire<OnShowInterAdSignal>();
        }
        

        public void Dispose()
        {
            UnsubscribeSignals();
        }
        
        private void SubscribeSignals()
        {
            _signalBus.Subscribe<OnNextDialogSignal>(NextDialog);
            _signalBus.Subscribe<OnHealthBuyButtonClick>(RestoreHealth);
            _signalBus.Subscribe<EndInterAdSignal>(CheckCurrentLevel);
        }
        
        private void UnsubscribeSignals()
        {
            _signalBus.Unsubscribe<OnHealthBuyButtonClick>(RestoreHealth);
            _signalBus.Unsubscribe<OnNextDialogSignal>(NextDialog);
            _signalBus.Unsubscribe<EndInterAdSignal>(CheckCurrentLevel);
        }

        private void StartChapterDialog()
        {
           _gameUiManager.HideGameUi();
           SetDialogsConfigs();
           _currentDialog = _chapterDialogFactory.Create(_dialogConfigs[_dialogIndex]); 
           _currentDialog.Initialize();
        }

        private void SetDialogsConfigs()
        {
            _currentChapterDialogConfigs = FindChapterDialogConfigByLevelNumber(_saveSystem.Data.CurrentLevelNumber);
            _dialogConfigs = _currentChapterDialogConfigs.Dialogs;
        }

        private void NextDialog()
        {
            _dialogIndex++;
            if (_dialogIndex>=_dialogConfigs.Length)
            {
                _dialogIndex = 0;
                _currentDialog.CloseChapterDialog();
                _currentDialog = null;
                _gameUiManager.ShowGameUi();
            }
            else
            {
                _currentDialog = _chapterDialogFactory.Create(_dialogConfigs[_dialogIndex]);
                _currentDialog.Initialize();
            }
        }

        private void InitPlayer()
        {
            _player.Initialize(LetCurrentLevelPosition(_saveSystem.Data.IsNeedToMove));
            _signalBus.Fire<OnCameraInitSignal>();
        }
        
        
        private Vector2 LetCurrentLevelPosition(bool isNeedToMove)
        {
            if (!isNeedToMove)
                return _chapterMapController.Levels[_saveSystem.Data.CurrentLevelNumber-1].LocalPosition;
            
            return _chapterMapController.Levels[_saveSystem.Data.CurrentLevelNumber-2].LocalPosition;
        }

        public void NextLevel()
        {
            _saveSystem.Data.IsNeedToMove = false;
            _saveSystem.SaveData();
            _player.MoveToNextLevel(LetCurrentLevelPosition(_saveSystem.Data.IsNeedToMove));
            CheckLevelDialog();
        }

        private void CheckCurrentLevel()
        {
            var level = _saveSystem.Data.CurrentLevelNumber;
            if (!_saveSystem.Data.IsNeedToMove &&
                level <= _chapterMapController.Levels.Length)
            {
                InitPlayer();
                CheckLevelDialog();
            }
            else if (_saveSystem.Data.IsNeedToMove &&
                     level <= _chapterMapController.Levels.Length)
            {
                InitPlayer();
                NextLevel();
            }
            else if (level > _chapterMapController.Levels.Length)
            {
                _saveSystem.Data.IsGameEnded = true;
                _saveSystem.SaveData();
                InitPlayer();
                _signalBus.Fire<ComingSoonSignal>();
            }
        }

        private void CheckLevelDialog()
        {
            if (_chapterMapController.Levels[_saveSystem.Data.CurrentLevelNumber-1].IsDialog)
            {
                StartChapterDialog();
            }
        }
        
        private ChapterDialogConfig FindChapterDialogConfigByLevelNumber(int level)
        {
            return _chapterDialogConfigs.First(config => config.LevelNumber == level);
        }

        private void RestoreHealth()
        {
            if (_saveSystem.Data.Gold >= HEALTH_RESTORE_PRICE && _saveSystem.Data.HealthValue == 0)
            {
                _saveSystem.Data.Gold -= HEALTH_RESTORE_PRICE;
                _saveSystem.Data.HealthValue = _saveSystem.Data.DEFAULT_HEALTH_VALUE;
                _saveSystem.SaveData();
                _signalBus.Fire(new OnHealthBuySignal(_saveSystem.Data.HealthValue, HEALTH_RESTORE_PRICE));
                _gameUiManager.UpdateUiValues();
            }
            else
            {
                _gameUiManager.ShowShopPanel();
            }
        }
    }
}