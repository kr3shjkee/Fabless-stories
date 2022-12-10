using Loading;
using Signals.Loading;
using UnityEngine;
using Zenject;

namespace Installers.LoadingScene
{
    public class LoadingSceneInstaller : MonoInstaller
    {
        [SerializeField] private UiPanelsController uiPanelControllerPrefab;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LoadingUiManager>().AsSingle().NonLazy();
            Container.Bind<UiPanelsController>().FromComponentInNewPrefab(uiPanelControllerPrefab).AsSingle().NonLazy();
            BindSignals();
        }

        private void BindSignals()
        {
            Container.DeclareSignal<OnStartButtonClickSignal>();
            Container.DeclareSignal<OnBackButtonClickSignal>();
            Container.DeclareSignal<OnExitButtonClickSignal>();
            Container.DeclareSignal<OnOptionsButtonClickSignal>();
        }
    }
}