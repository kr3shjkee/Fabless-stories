using UnityEngine;

namespace Signals.Ui
{
    public class OnCloseCurrentPanelSignal
    {
        public readonly GameObject _currentPanel;

        public OnCloseCurrentPanelSignal(GameObject currentPanel)
        {
            _currentPanel = currentPanel;
        }
    }
}