using System;
using GameUi;
using TMPro;
using UnityEngine;
using Zenject;

namespace Common
{
    public abstract class BaseTimer : MonoBehaviour
    {
        [SerializeField] protected int delayInHours;
        [SerializeField] protected TextMeshProUGUI timerText;
        
        protected SaveSystem _saveSystem;
        protected GameUiManager _gameUiManager;

        protected DateTime _targetTime;
        
        [Inject]
        public virtual void Construct(SaveSystem saveSystem, GameUiManager gameUiManager)
        {
            _saveSystem = saveSystem;
            _gameUiManager = gameUiManager;
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