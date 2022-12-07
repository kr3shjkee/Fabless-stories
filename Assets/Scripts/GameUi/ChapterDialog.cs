using System.Collections;
using System.Collections.Generic;
using Common;
using Configs;
using Signals.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
        [SerializeField] private Image characterSpr1;
        [SerializeField] private Image characterSpr2;
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private TextMeshProUGUI chapterName;
        
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
            chapterName.text = _dialogConfig.CharacterName;
            if (_dialogConfig.IsCharacterPositionLeft())
            {
                characterSpr1.enabled = true;
                characterSpr1.sprite = _dialogConfig.CharacterSpr;
            }
            else
            {
                characterSpr2.enabled = true;
                characterSpr2.sprite = _dialogConfig.CharacterSpr;
            }
            
            if (!_isTextFull)
                StartCoroutine(WriteDialogText());
        }

        private void ClearCurrentDialog()
        {
            characterSpr1.enabled = false;
            characterSpr2.enabled = false;
            chapterName.text = "";
            dialogText.text = "";
            _isTextFull = false;
        }

        private IEnumerator WriteDialogText()
        {
            _soundManager.KeyboardSoundPlay();
            int index = 0;
            List<char> list = new List<char>();
            while (index < _dialogConfig.CharacterText.Length)
            {
                list.Add(_dialogConfig.CharacterText[index]);
                dialogText.text += list[index];
                index++;
                yield return new WaitForSeconds(TEXT_WRITING_DELAY);
            }
            _soundManager.KeyboardSoundStop();
            _isTextFull = true;
        }

        private void CheckSignal()
        {
            if (!_isTextFull)
            {
                GetFullText();
                _soundManager.KeyboardSoundStop();
            }
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