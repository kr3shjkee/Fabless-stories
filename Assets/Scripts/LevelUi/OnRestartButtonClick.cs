using System;
using Common;
using Signals.Level;

namespace LevelUi
{
    public class OnRestartButtonClick : BaseButtonController
    {
        private void Update()
        {
            if (_saveSystem.Data.HealthValue > 0)
                _button.interactable = true;
            else
                _button.interactable = false;
        }

        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnRestartSignal>();
        }
    }
}