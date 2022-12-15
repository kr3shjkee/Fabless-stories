using Common;
using Signals.Ui;
using UnityEngine.UI;

namespace GameUi
{
    public class OnCloseShopPanelClick : BaseButtonController
    {

        protected override void Awake()
        {
            _button = GetComponent<Button>();
        }

        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnShopPanelCloseSignal>();
        }
    }
}