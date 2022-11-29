using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common
{
    public abstract class BaseButtonController : MonoBehaviour
    {
        protected SignalBus _signalBus;
        private SoundManager _soundManager;
        
        [Inject]
        public void Construct(SignalBus signalBus, SoundManager soundManager)
        {
            _signalBus = signalBus;
            _soundManager = soundManager;
        }

        private Button _button;
        
        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
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
