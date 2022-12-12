using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Common
{
    public abstract class BaseTimer : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI timerText;
        
        protected SaveSystem _saveSystem;
        protected SignalBus _signalBus;

        protected DateTime _targetTime;
        
        [Inject]
        public virtual void Construct(SaveSystem saveSystem, SignalBus signalBus)
        {
            _saveSystem = saveSystem;
            _signalBus = signalBus;
        }

        protected virtual void Start()
        {
            _saveSystem.LoadData();
        }

        protected abstract void Update();

        protected abstract void OnDestroy();

        protected abstract void CheckTime();
    }
}