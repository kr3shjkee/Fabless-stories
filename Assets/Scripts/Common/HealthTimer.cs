﻿using System;
using Signals.Ui;
using UnityEngine;

namespace Common
{
    public class HealthTimer : BaseTimer
    {
        protected override void Start()
        {
            base.Start();
            var currentTime = DateTime.Now;
            _targetTime = currentTime.AddHours(delayInHours);
            if (_saveSystem.Data.HealthTimer != _saveSystem.Data.DEFAULT_HEALTH_TIMER)
            {
                timerText.gameObject.SetActive(true);
                _targetTime = DateTime.Parse(_saveSystem.Data.HealthTimer);
                CheckTime();
            }
        }

        protected override void Update()
        {
            if (_saveSystem.Data.HealthValue < _saveSystem.Data.DEFAULT_HEALTH_VALUE)
            {
                CheckTime();
                TimeSpan time = _targetTime - DateTime.Now;
                var hours = time.Hours.ToString() + ":";
                var mins = time.Minutes.ToString() + ":";
                var secs = time.Seconds.ToString();
                timerText.text = hours + mins + secs;
            }
        }

        protected override void OnDestroy()
        {
            if (_saveSystem.Data.HealthValue >= _saveSystem.Data.DEFAULT_HEALTH_VALUE) 
                return;
            
            _saveSystem.Data.HealthTimer = _targetTime.ToString();
            _saveSystem.SaveData();
        }

        protected override void CheckTime()
        {
            var time = _targetTime - DateTime.Now;

            if (time.Seconds < 0)
            {
                timerText.gameObject.SetActive(false);
                _saveSystem.Data.HealthValue = _saveSystem.Data.DEFAULT_HEALTH_VALUE;
                _saveSystem.Data.HealthTimer = _saveSystem.Data.DEFAULT_HEALTH_TIMER;
                _saveSystem.SaveData();
                _signalBus.Fire<OnUpdateUiValuesSignal>();
            }
        }
    }
}