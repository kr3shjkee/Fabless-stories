using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class GameUiPanelsController : MonoBehaviour
    {
        [SerializeField] private GameObject healthPanel;
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private GameObject optionsPanel;
        [SerializeField] private GameObject soundOptionsPanel;
        [SerializeField] private Button[] uiButtons;

        public void ShowHealthPanel()
        {
            healthPanel.SetActive(true);
            InteractableButtonsDeactivate();
        }

        public void ShowShopPanel()
        {
            shopPanel.SetActive(true);
            InteractableButtonsDeactivate();
        }

        public void ShowOptionsPanel()
        {
            optionsPanel.SetActive(true);
            InteractableButtonsDeactivate();
        }

        public void ShowSoundOptionsPanel()
        {
            soundOptionsPanel.SetActive(true);
            InteractableButtonsDeactivate();
        }

        public void CloseCurrentPanel(GameObject currentPanel)
        {
            currentPanel.SetActive(false);
            
            if (currentPanel!=soundOptionsPanel)
                InteractableButtonsActivate();
        }

        private void InteractableButtonsActivate()
        {
            foreach (var button in uiButtons)
            {
                button.interactable = true;
            }
        }

        private void InteractableButtonsDeactivate()
        {
            foreach (var button in uiButtons)
            {
                button.interactable = false;
            }
        }
    }
}