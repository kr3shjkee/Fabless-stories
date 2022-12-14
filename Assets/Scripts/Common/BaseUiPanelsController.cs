using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public abstract class BaseUiPanelsController : MonoBehaviour
    {
        [SerializeField] protected GameObject healthPanel;
        [SerializeField] protected GameObject shopPanel;
        [SerializeField] protected GameObject optionsPanel;
        [SerializeField] protected GameObject soundOptionsPanel;
        [SerializeField] protected Button[] uiButtons;
        [SerializeField] protected GameObject uiCanvas;
        [SerializeField] protected TextMeshProUGUI healthValue;
        [SerializeField] protected TextMeshProUGUI goldValue;
        
        public virtual void ShowHealthPanel()
        {
            healthPanel.SetActive(true);
            InteractableButtonsDeactivate();
        }

        public virtual void ShowShopPanel()
        {
            shopPanel.SetActive(true);
            InteractableButtonsDeactivate();
        }

        public virtual void ShowOptionsPanel()
        {
            optionsPanel.SetActive(true);
            InteractableButtonsDeactivate();
        }

        public virtual void ShowSoundOptionsPanel()
        {
            soundOptionsPanel.SetActive(true);
            InteractableButtonsDeactivate();
        }

        public virtual void CloseCurrentPanel(GameObject currentPanel)
        {
            currentPanel.SetActive(false);
            
            if (currentPanel!=soundOptionsPanel)
                InteractableButtonsActivate();
        }

        public virtual void CloseShopPanel()
        {
            shopPanel.SetActive(false);
            InteractableButtonsActivate();
        }
        

        protected virtual void InteractableButtonsActivate()
        {
            foreach (var button in uiButtons)
            {
                button.interactable = true;
            }
        }

        protected virtual void InteractableButtonsDeactivate()
        {
            foreach (var button in uiButtons)
            {
                button.interactable = false;
            }
        }

        public virtual void CloseUi()
        {
            uiCanvas.SetActive(false);
        }

        public virtual void OpenUi()
        {
            uiCanvas.SetActive(true);
        }

        public virtual void UpdateHealthValue(int value)
        {
            healthValue.text = value.ToString();
        }

        public virtual void UpdateGoldValue(int value)
        {
            goldValue.text = value.ToString();
        }
    }
}