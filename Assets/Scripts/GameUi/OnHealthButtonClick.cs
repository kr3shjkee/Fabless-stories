using System;
using Common;
using Signals.Ui;
using UnityEngine;

namespace GameUi
{
    public class OnHealthButtonClick : BaseButtonController
    {
        [SerializeField] private GameObject plusImage;
        
        private void Update()
        {
            if (_saveSystem.Data.HealthValue < 1)
            {
                plusImage.SetActive(true);
                _button.interactable = true;
            }
            else
            {
                plusImage.SetActive(false);
                _button.interactable = false;
            }
        }

        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnHealthButtonClickSignal>();
        }
    }
}