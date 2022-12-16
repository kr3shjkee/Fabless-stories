using Ads;
using Common;
using Signals.Ads;
using Signals.Analytics;
using Signals.Loading;
using UnityEngine;
using Zenject;

namespace Installers.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private SoundManager soundManagerPrefab;
        [SerializeField] private AdsManager adsManagerPrefab;
        [SerializeField] private AnalyticsManager analyticsManager;
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.BindInterfacesAndSelfTo<ProjectSetup>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
            Container.Bind<SoundManager>().FromComponentInNewPrefab(soundManagerPrefab).AsSingle().NonLazy();
            Container.Bind<AdsManager>().FromComponentInNewPrefab(adsManagerPrefab).AsSingle().NonLazy();
            Container.Bind<AnalyticsManager>().FromComponentInNewPrefab(analyticsManager).AsSingle().NonLazy();
        
            BindSignals();
            BindAnalyticSignals();
        }
    
        private void BindSignals()
        {
            Container.DeclareSignal<OnAgreeButtonClickSignal>();
            Container.DeclareSignal<OnSoundOptionChangedSignal>();
            Container.DeclareSignal<OnMusicOptionChangedSignal>();
            Container.DeclareSignal<OnShowInterAdSignal>();
            Container.DeclareSignal<OnShowRewardedAdSignal>();
            Container.DeclareSignal<EndInterAdSignal>();
            Container.DeclareSignal<EndRewardedAdSignal>();
        }

        private void BindAnalyticSignals()
        {
            Container.DeclareSignal<OnLevelCompleteSignal>();
            Container.DeclareSignal<OnLevelStepsBuySignal>();
            Container.DeclareSignal<OnBackStepsBuySignal>();
            Container.DeclareSignal<OnHealthBuySignal>();
            Container.DeclareSignal<OnLevelFailSignal>();
            Container.DeclareSignal<OnBuyAdRewardItemsSignal>();
        }
    }
}