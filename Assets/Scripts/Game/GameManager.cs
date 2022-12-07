using System;
using System.Linq;
using Common;
using Configs;
using GameUi;
using Signals.Game;
using Signals.Ui;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameManager : IInitializable, IDisposable
    {
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
        }
        

        public void Dispose()
        {
            UnsubscribeSignals();
        }
        
        private void SubscribeSignals()
        {
            _signalBus.Subscribe<OnNextDialogSignal>(NextDialog);
            _signalBus.Subscribe<EndLevelMapInitializeSignal>(InitPlayer);
            _signalBus.Subscribe<OnPlayLevelButtonClickSignal>(NextLevel);
        }
        
        private void UnsubscribeSignals()
        {
            _signalBus.Unsubscribe<OnNextDialogSignal>(NextDialog);
            _signalBus.Unsubscribe<EndLevelMapInitializeSignal>(InitPlayer);
            _signalBus.Unsubscribe<OnPlayLevelButtonClickSignal>(NextLevel);
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
            _player.Initialize(LetCurrentLevelPosition());
            CheckLevelDialog();
        }
        
        private Vector2 LetCurrentLevelPosition()
        {
            return _chapterMapController.Levels[_saveSystem.Data.CurrentLevelNumber-1].LocalPosition;
        }

        public void NextLevel()
        {
            if (_saveSystem.Data.CurrentLevelNumber < _chapterMapController.Levels.Length)
            {
                _saveSystem.Data.CurrentLevelNumber++;
                _saveSystem.SaveData();
                _player.MoveToNextLevel(LetCurrentLevelPosition());
                CheckLevelDialog();
            }
            else
                _signalBus.Fire<ComingSoonSignal>();
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
        
    }
}