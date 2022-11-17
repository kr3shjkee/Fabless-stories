using UnityEngine;

namespace Loading
{
    public class UiPanelsController : MonoBehaviour
    {
        [SerializeField] private GameObject termOfUsePanel;
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject optionsPanel;

        public void ShowMainMenuPanel()
        {
            termOfUsePanel.SetActive(false);
            optionsPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }

        public void ShowOptionsPanel()
        {
            termOfUsePanel.SetActive(false);
            mainMenuPanel.SetActive(false);
            optionsPanel.SetActive(true);
        }

        public void ShowTermOfUsePanel()
        {
            termOfUsePanel.SetActive(true);
            mainMenuPanel.SetActive(false);
            optionsPanel.SetActive(false);
        }
    }
}