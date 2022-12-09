using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common
{
    public abstract class BaseButtonController : MonoBehaviour
    {
        protected SignalBus _signalBus;
        private SoundManager _soundManager;
        protected SaveSystem _saveSystem;
        
        [Inject]
        public void Construct(SignalBus signalBus, SoundManager soundManager, SaveSystem saveSystem)
        {
            _signalBus = signalBus;
            _soundManager = soundManager;
            _saveSystem = saveSystem;
        }

        protected Button _button;
        
        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
            _saveSystem.LoadData();
        }

        protected virtual void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        protected virtual void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        protected virtual void OnClick()
        {
            _soundManager.ButtonClickSoundPlay();
        }
    }
}
