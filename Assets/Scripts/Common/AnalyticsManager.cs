using UnityEngine;
using GameAnalyticsSDK;
using Signals.Analytics;
using Zenject;

namespace Common
{
    public class AnalyticsManager : MonoBehaviour
    {
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        private void Awake()
        {
            SubscribeSignals();
            GameAnalytics.Initialize();
        }

        private void OnDestroy()
        {
            UnsubscribeSignals();
        }

        private void SubscribeSignals()
        {
            _signalBus.Subscribe<OnLevelCompleteSignal>(LevelComplete);
            _signalBus.Subscribe<OnLevelFailSignal>(LevelFail);
            _signalBus.Subscribe<OnBackStepsBuySignal>(BuyBackSteps);
            _signalBus.Subscribe<OnLevelStepsBuySignal>(BuyLevelSteps);
            _signalBus.Subscribe<OnHealthBuySignal>(BuyHealth);
            _signalBus.Subscribe<OnBuyAdRewardItemsSignal>(BuyAdRewardItem);
        }
        
        private void UnsubscribeSignals()
        {
            _signalBus.Unsubscribe<OnLevelCompleteSignal>(LevelComplete);
            _signalBus.Unsubscribe<OnLevelFailSignal>(LevelFail);
            _signalBus.Unsubscribe<OnBackStepsBuySignal>(BuyBackSteps);
            _signalBus.Unsubscribe<OnLevelStepsBuySignal>(BuyLevelSteps);
            _signalBus.Unsubscribe<OnHealthBuySignal>(BuyHealth);
            _signalBus.Unsubscribe<OnBuyAdRewardItemsSignal>(BuyAdRewardItem);
        }

        private void BuyAdRewardItem(OnBuyAdRewardItemsSignal signal)
        {
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Gold", signal.GoldValue,"AdRewardItem", 
                signal.ItemId.ToString());
        }

        private void BuyHealth(OnHealthBuySignal signal)
        {
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Health", signal.HealthValue,
                "HealthMaxValue", "");
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Gold", signal.Goldlost,"GoldForHealth", "");
        }

        private void BuyLevelSteps(OnLevelStepsBuySignal signal)
        {
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "LevelSteps", signal.StepsCount,
                "LevelStepsForGold", "");
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Gold", signal.GoldLost,"GoldForLevelSteps", "");
        }

        private void BuyBackSteps(OnBackStepsBuySignal signal)
        {
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "BackSteps", signal.BackStepsCount,
                "BackStepsOnLevelForGold", "");
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Gold", signal.GoldLost,"GoldForBackSteps", "");
        }

        private void LevelComplete(OnLevelCompleteSignal signal)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level: " + signal.LevelNumber,
                "Not used steps: " + signal.NonUsedSteps);
        }

        private void LevelFail(OnLevelFailSignal signal)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level: " + signal.LevelNumber);
        }
    }
}