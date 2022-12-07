using Configs;
using Game;
using GameUi;
using Signals.Game;
using Signals.Ui;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Installers.GameScene
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameUiPanelsController gameUiPanelsControllerPrefab;
        [SerializeField] private ChapterDialog chapterDialogPrefab;
        [SerializeField] private MapLevel mapLevelPrefab;
        [SerializeField] private PlayerController playerControllerPrefab;
        public override void InstallBindings()
        {
            InstallGameBindings();
            BindGameSignals();
            
            InstallUiBindings();
            BindUiSignals();
        }

        private void InstallGameBindings()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ChapterMapController>().AsSingle().NonLazy();
            Container.BindFactory<LevelMapConfig, MapLevelPosition, MapLevel, MapLevel.Factory>()
                .FromComponentInNewPrefab(mapLevelPrefab);
            Container.Bind<PlayerController>().FromComponentInNewPrefab(playerControllerPrefab).AsSingle().NonLazy();
        }
        
        private void BindGameSignals()
        {
            Container.DeclareSignal<EndLevelMapInitializeSignal>();
            Container.DeclareSignal<OnPlayLevelButtonClickSignal>();
            Container.DeclareSignal<ComingSoonSignal>();
        }

        private void InstallUiBindings()
        {
            Container.BindInterfacesAndSelfTo<GameUiManager>().AsSingle().NonLazy();
            Container.Bind<GameUiPanelsController>().FromComponentInNewPrefab(gameUiPanelsControllerPrefab).AsSingle().NonLazy();
            Container.BindFactory<DialogConfig, ChapterDialog, ChapterDialog.Factory>()
                .FromComponentInNewPrefab(chapterDialogPrefab);
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
        
    }
}