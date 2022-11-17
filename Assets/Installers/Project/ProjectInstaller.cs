using Loading;
using Signals.Loading;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.BindInterfacesAndSelfTo<ProjectSetup>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        BindSignals();
    }
    
    private void BindSignals()
    {
        Container.DeclareSignal<OnAgreeButtonClickSignal>();
    }
}