using System;
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
        private int index = 0;
        private ChapterDialog _currentDialog;

        private DialogConfig[] _dialogConfigs;

        public GameManager(SignalBus signalBus, SaveSystem saveSystem, GameUiManager gameUiManager, 
            ChapterDialog.Factory ChapterDialogFactory, ChapterDialogConfig[] chapterDialogConfigs,
            PlayerController player, ChapterMapController chapterMapController)
        {
            _signalBus = signalBus;
            _saveSystem = saveSystem;
            _gameUiManager = gameUiManager;
            _chapterDialogFactory = ChapterDialogFactory;
            _chapterDialogConfigs = chapterDialogConfigs;
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
           _currentDialog = _chapterDialogFactory.Create(_dialogConfigs[index]);
           _currentDialog.Initialize();
        }

        private void SetDialogsConfigs() //TODO: добавить логику расширения
        {
            _currentChapterDialogConfigs = _chapterDialogConfigs[0];
            _dialogConfigs = _currentChapterDialogConfigs.Dialogs;
        }

        private void NextDialog()
        {
            index++;
            if (index>=_dialogConfigs.Length)
            {
                index = 0;
                _currentDialog.CloseChapterDialog();
                _currentDialog = null;
                _gameUiManager.ShowGameUi();
            }
            else
            {
                _currentDialog = _chapterDialogFactory.Create(_dialogConfigs[index]);
                _currentDialog.Initialize();
            }
        }

        private void InitPlayer()
        {
            _player.Initialize(LetCurrentLevelPosition());
            if (_chapterMapController.Levels[_saveSystem.Data.CurrentLevelNumber].IsDialog)
            {
                StartChapterDialog();
            }
        }
        
        private Vector2 LetCurrentLevelPosition()
        {
            return _chapterMapController.Levels[_saveSystem.Data.CurrentLevelNumber].LocalPosition;
        }

        public void NextLevel()
        {
            _saveSystem.Data.CurrentLevelNumber++;
            _player.MoveToNextLevel(LetCurrentLevelPosition());
        }

        public void BackLevel()
        {
            _saveSystem.Data.CurrentLevelNumber--;
            _player.MoveToNextLevel(LetCurrentLevelPosition());
        }
    }
}