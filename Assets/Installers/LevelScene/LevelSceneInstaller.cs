using Configs;
using Level;
using Signals.Level;
using Signals.Ui;
using UnityEngine;
using Zenject;

namespace Installers.LevelScene
{
    public class LevelSceneInstaller : MonoInstaller
    {
        [SerializeField] private Element elementPrefab;
        [SerializeField] private LevelUiPanelsController levelUiPanelsController;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BoardController>().AsSingle().NonLazy();
            Container.BindFactory<ElementConfigItem, ElementPosition, Element, Element.Factory>()
                .FromComponentInNewPrefab(elementPrefab);
            Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelUiManager>().AsSingle().NonLazy();
            Container.Bind<LevelUiPanelsController>().FromComponentInNewPrefab(levelUiPanelsController).AsSingle().NonLazy();
            BindLogicSignals();
            BindUiSignals();
        }
    
        private void BindLogicSignals()
        {
            Container.DeclareSignal<OnElementClickSignal>();
            Container.DeclareSignal<OnBoardMatchSignal>();
            Container.DeclareSignal<OnRestartSignal>();
            Container.DeclareSignal<OnScoreChangedSignal>();
            Container.DeclareSignal<OnElementMatchShowSignal>();
            Container.DeclareSignal<OnDoStepSignal>();
            Container.DeclareSignal<OnBackStepSignal>();
        }

        private void BindUiSignals()
        {
            Container.DeclareSignal<OnCloseCurrentPanelSignal>();
            Container.DeclareSignal<OnExitButtonClickSignal>();
            Container.DeclareSignal<OnHealthButtonClickSignal>();
            Container.DeclareSignal<OnShopButtonClickSignal>();
            Container.DeclareSignal<OnOptionsButtonClickSignal>();
            Container.DeclareSignal<OnSoundOptionsButtonClickSignal>();
        }
    }
}