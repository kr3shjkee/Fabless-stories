using Common;
using Signals.Ui;
using UnityEngine;

namespace GameUi
{
    public class OnCloseCurrentPanelClick : BaseButtonController
    {
        [SerializeField] private GameObject currentPanel;
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire(new OnCloseCurrentPanelSignal(currentPanel));
        }
    }
}