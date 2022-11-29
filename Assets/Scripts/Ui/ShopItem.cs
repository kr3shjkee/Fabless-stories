using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Button = UnityEngine.UI.Button;

namespace Ui
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Image selectedBg;
        [SerializeField] private TextMeshProUGUI priceValue;
        [SerializeField] private TextMeshProUGUI cooldownValue;

        private Button _button;

        private SignalBus _signalBus;
        
        public string ID { get; private set; }

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        private void Awake()
        {
            _button.GetComponent<Button>();
        }
        
        private void Start()
        {
            _button.onClick.AddListener(OnClick);
        }
         
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            
        }
        
        public void SetSelected(bool isSelected)
        {
            selectedBg.enabled = isSelected;
        }

        // public void Setup(StoreItemConfig config)
        // {
        //     priceValue.text = config.Price.ToString();
        // }
    }
}