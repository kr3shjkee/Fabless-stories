using Game;
using GameUi;
using Signals.Ui;
using UnityEngine;
using Zenject;

namespace Installers.GameScene
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameUiPanelsController gameUiPanelsControllerPrefab;
        [SerializeField] private ChapterDialog chapterDialogPrefab;
        public override void InstallBindings()
        {
            InstallGameBindings();
            BindGameSignals();
            
            InstallUiBindings();
            BindUiSignals();
            
            InstallLevelBindings();
            BindLevelSignals();
        }

        private void InstallGameBindings()
        {
            
        }
        
        private void BindGameSignals()
        {
        
        }

        private void InstallUiBindings()
        {
            Container.BindInterfacesAndSelfTo<GameUiManager>().AsSingle().NonLazy();
            Container.Bind<GameUiPanelsController>().FromComponentInNewPrefab(gameUiPanelsControllerPrefab).AsSingle().NonLazy();
            Container.BindFactory<DialogConfig, ChapterDialog, ChapterDialog.Factory>()
                .FromComponentInNewPrefab(chapterDialogPrefab);
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
        }

        private void BindUiSignals()
        {
            Container.DeclareSignal<OnCloseCurrentPanelSignal>();
            Container.DeclareSignal<OnExitButtonClickSignal>();
            Container.DeclareSignal<OnHealthButtonClickSignal>();
            Container.DeclareSignal<OnShopButtonClickSignal>();
            Container.DeclareSignal<OnOptionsButtonClickSignal>();
            Container.DeclareSignal<OnSoundOptionsButtonClickSignal>();
            Container.DeclareSignal<OnNextButtonClickSignal>();
            Container.DeclareSignal<OnNextDialogSignal>();
        }
        private void InstallLevelBindings()
        {
            
        }
        
        private void BindLevelSignals()
        {
        
        }
    }
}