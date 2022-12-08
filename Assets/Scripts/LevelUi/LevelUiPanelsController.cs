using Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelUi
{
    public class LevelUiPanelsController : BaseUiPanelsController
    {

        [SerializeField] private GameObject backStepsRestorePanel;
        [SerializeField] private GameObject failPanel;
        [SerializeField] private Canvas goldCanvas;
        [SerializeField] private TextMeshProUGUI targetValue;
        [SerializeField] private TextMeshProUGUI backStepsValue;
        [SerializeField] private TextMeshProUGUI stepsValue;
        [SerializeField] private Image targetIcon;
        [SerializeField] private GameObject winPanel;
        

        public void UpdateTargetIcon(Sprite icon)
        {
            targetIcon.sprite = icon;
        }

        public void UpdateTargetValue(int value)
        {
            targetValue.text = value.ToString();
        }

        public void UpdateBackStepsValue(int value)
        {
            backStepsValue.text = value.ToString();
        }

        public void UpdateStepsValue(int value)
        {
            stepsValue.text = value.ToString();
        }

        public void ShowBackStepsRestorePanel()
        {
            backStepsRestorePanel.SetActive(true);
        }

        public void ShowFailPanel()
        {
            OverrideSortingGoldCanvas(true);
            failPanel.SetActive(true);
        }

        public void HideFailPanel()
        {
            OverrideSortingGoldCanvas(false);
            failPanel.SetActive(false);
        }

        public void HideBackStepsRestorePanel()
        {
            backStepsRestorePanel.SetActive(false);
        }

        private void OverrideSortingGoldCanvas(bool isOverride)
        {
            if (isOverride)
                goldCanvas.overrideSorting = true;
            else
                goldCanvas.overrideSorting = false;
        }

        public void ShowWinPanel()
        {
            winPanel.SetActive(true);
        }
    }
}