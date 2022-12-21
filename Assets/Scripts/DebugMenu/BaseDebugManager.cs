using Common;
using Signals.DebugMenu;
using UnityEngine;
using Zenject;

namespace DebugMenu
{
    public class BaseDebugManager : MonoBehaviour
    {
        protected const int GOLD_TEN_K = 10000;
        
        [SerializeField] protected GameObject debugPanel;
        
        protected SignalBus _signalBus;

        protected SaveSystem _saveSystem;

        protected virtual void Start()
        {
            _saveSystem.LoadData();
            _signalBus.Subscribe<OnCloseDebugClick>(CloseDebug);
            _signalBus.Subscribe<OnHealthDownSignal>(HealthLost);
            _signalBus.Subscribe<OnHealthFullSignal>(HealthFull);
            _signalBus.Subscribe<OnGoldZeroSignal>(GoldLost);
            _signalBus.Subscribe<OnGoldRichSignal>(GoldTenK);
        }

        protected virtual void OnDestroy()
        {
            _signalBus.Unsubscribe<OnCloseDebugClick>(CloseDebug);
            _signalBus.Unsubscribe<OnHealthDownSignal>(HealthLost);
            _signalBus.Unsubscribe<OnHealthFullSignal>(HealthFull);
            _signalBus.Unsubscribe<OnGoldZeroSignal>(GoldLost);
            _signalBus.Unsubscribe<OnGoldRichSignal>(GoldTenK);
        }

        protected void Update()
        {
            if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.W))
            {
                debugPanel.SetActive(true);
            }
        }

        protected void CloseDebug()
        {
            debugPanel.SetActive(false);
        }
        
        protected virtual void HealthLost()
        {
            _saveSystem.Data.HealthValue = 0;
            _saveSystem.SaveData();
            _signalBus.Fire<OnCheckHealthTimerSignal>();
            CloseDebug();
        }

        protected virtual void HealthFull()
        {
            _saveSystem.Data.HealthValue = _saveSystem.Data.DEFAULT_HEALTH_VALUE;
            _saveSystem.SaveData();
            _signalBus.Fire<OnCheckHealthTimerSignal>();
            CloseDebug();
        }

        protected virtual void GoldLost()
        {
            _saveSystem.Data.Gold = 0;
            _saveSystem.SaveData();
            CloseDebug();
        }

        protected virtual void GoldTenK()
        {
            _saveSystem.Data.Gold = GOLD_TEN_K;
            _saveSystem.SaveData();
            CloseDebug();
        } 
    }
}