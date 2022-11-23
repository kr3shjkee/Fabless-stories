using Common;
using Loading;
using Signals.Loading;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private SoundManager soundManagerPrefab;
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.BindInterfacesAndSelfTo<ProjectSetup>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        Container.Bind<SoundManager>().FromComponentInNewPrefab(soundManagerPrefab).AsSingle().NonLazy();
        BindSignals();
    }
    
    private void BindSignals()
    {
        Container.DeclareSignal<OnAgreeButtonClickSignal>();
    }
}