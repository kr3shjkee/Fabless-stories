using System;
using Common;
using GameUi;
using Signals.Ui;
using Zenject;

namespace Game
{
    public class GameManager : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly SaveSystem _saveSystem;
        private readonly GameUiManager _gameUiManager;
        private readonly ChapterDialog.Factory _factory;
        private readonly ChapterDialogConfig[] _chapterDialogConfigs;

        private ChapterDialogConfig _currentChapterDialogConfigs;
        private int index = 0;
        private ChapterDialog _currentDialog;

        private DialogConfig[] _dialogConfigs;

        public GameManager(SignalBus signalBus, SaveSystem saveSystem, GameUiManager gameUiManager, ChapterDialog.Factory factory, ChapterDialogConfig[] chapterDialogConfigs)
        {
            _signalBus = signalBus;
            _saveSystem = saveSystem;
            _gameUiManager = gameUiManager;
            _factory = factory;
            _chapterDialogConfigs = chapterDialogConfigs;
        }
        
        public void Initialize()
        {
            SubscribeSignals();
            StartChapterDialog();
        }
        

        public void Dispose()
        {
            UnsubscribeSignals();
        }
        
        private void SubscribeSignals()
        {
            _signalBus.Subscribe<OnNextDialogSignal>(NextDialog);
        }
        
        private void UnsubscribeSignals()
        {
            _signalBus.Unsubscribe<OnNextDialogSignal>(NextDialog);
        }

        private void StartChapterDialog()
        {
           _gameUiManager.HideGameUi();
           SetDialogsConfigs();
           _currentDialog = _factory.Create(_dialogConfigs[index]);
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
                _currentDialog = _factory.Create(_dialogConfigs[index]);
                _currentDialog.Initialize();
            }
        }
    }
}