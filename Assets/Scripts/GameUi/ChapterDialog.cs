using System.Collections;
using System.Collections.Generic;
using Common;
using Signals.Ui;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace GameUi
{
    public class ChapterDialog : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<DialogConfig, ChapterDialog>
        {
            
        }

        private const float TEXT_WRITING_DELAY = 0.1f;
        
        [SerializeField] private GameObject chaptersPanel;
        [SerializeField] private TextMeshProUGUI characterName;
        [SerializeField] private TextMeshProUGUI dialogText;

        private SignalBus _signalBus;
        private DialogConfig _dialogConfig;
        private SoundManager _soundManager;

        private bool _isTextFull;


        [Inject]
        public void Construct(DialogConfig dialogConfig, SoundManager soundManager, SignalBus signalBus)
        {
            _dialogConfig = dialogConfig;
            _soundManager = soundManager;
            _signalBus = signalBus;
        }

        private void Start()
        {
            _signalBus.Subscribe<OnNextButtonClickSignal>(CheckSignal);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnNextButtonClickSignal>(CheckSignal);
        }

        public void Initialize()
        {
            ClearCurrentDialog();
            characterName.text = _dialogConfig.CharacterName;
            Instantiate(_dialogConfig.Character, chaptersPanel.transform);
            if (!_isTextFull)
                StartCoroutine(WriteDialogText());
        }

        private void ClearCurrentDialog()
        {
            characterName.text = "";
            dialogText.text = "";
            foreach (Transform child in chaptersPanel.transform) 
                Destroy(child.gameObject);
            _isTextFull = false;
        }

        private IEnumerator WriteDialogText()
        {
            int index = 0;
            List<char> list = new List<char>();
            while (index < _dialogConfig.CharacterText.Length)
            {
                list.Add(_dialogConfig.CharacterText[index]);
                dialogText.text += list[index];
                index++;
                yield return new WaitForSeconds(TEXT_WRITING_DELAY);
            }

            _isTextFull = true;
        }

        private void CheckSignal()
        {
            if (!_isTextFull)
                GetFullText();
            else
            {
                _signalBus.Fire<OnNextDialogSignal>();
                CloseChapterDialog();
            }
                
        }
        
        private void GetFullText()
        {
            StopAllCoroutines();
            dialogText.text = _dialogConfig.CharacterText;
            _isTextFull = true;
        }

        public void CloseChapterDialog()
        {
            Destroy(gameObject);
        }
    }
}