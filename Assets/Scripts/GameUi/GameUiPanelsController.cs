using Common;
using UnityEngine;

namespace GameUi
{
    public class GameUiPanelsController : BaseUiPanelsController
    {
        [SerializeField] private GameObject comingSoonPanel;
        
        public void ShowComingSoonPanel()
        {
            comingSoonPanel.SetActive(true);
            InteractableButtonsDeactivate();
        }
        
    }
}