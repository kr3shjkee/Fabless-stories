using Common;
using UnityEngine;

namespace DebugMenu
{
    public class OnCloseDebugClick : BaseButtonController
    {
        protected override void OnClick()
        {
            base.OnClick();
            _signalBus.Fire<OnCloseDebugClick>();
        }
    }
}