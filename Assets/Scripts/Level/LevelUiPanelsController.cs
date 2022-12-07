using Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    public class LevelUiPanelsController : BaseUiPanelsController
    {
        [SerializeField] private GameObject backStepsRestorePanel;
        [SerializeField] private TextMeshProUGUI targetValue;
        [SerializeField] private TextMeshProUGUI backStepsValue;
        [SerializeField] private TextMeshProUGUI stepsValue;
        [SerializeField] private Image targetIcon;

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
    }
}