using Ads;
using Common;
using Signals.Ads;
using Signals.Loading;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SoundManager soundManagerPrefab;
    [SerializeField] private AdsManager adsManagerPrefab;
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.BindInterfacesAndSelfTo<ProjectSetup>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        Container.Bind<SoundManager>().FromComponentInNewPrefab(soundManagerPrefab).AsSingle().NonLazy();
        Container.Bind<AdsManager>().FromComponentInNewPrefab(adsManagerPrefab).AsSingle().NonLazy();
        BindSignals();
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
}