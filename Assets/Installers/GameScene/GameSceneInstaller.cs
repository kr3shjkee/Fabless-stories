using Common;
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
        [SerializeField] private ShopItem shopItemPrefab;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ChapterMapController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameUiManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
            Container.BindFactory<LevelMapConfig, MapLevelPosition, MapLevel, MapLevel.Factory>()
                .FromComponentInNewPrefab(mapLevelPrefab);
            Container.Bind<PlayerController>().FromComponentInNewPrefab(playerControllerPrefab).AsSingle().NonLazy();
            Container.Bind<GameUiPanelsController>().FromComponentInNewPrefab(gameUiPanelsControllerPrefab).AsSingle().NonLazy();
            Container.BindFactory<DialogConfig, ChapterDialog, ChapterDialog.Factory>()
                .FromComponentInNewPrefab(chapterDialogPrefab);

            Container.BindFactory<ItemConfig, ShopItem, ShopItem.Factory>()
                .FromComponentInNewPrefab(shopItemPrefab);
            Container.BindInterfacesAndSelfTo<ShopController>().AsSingle().NonLazy();
            
            BindGameSignals();
            BindUiSignals();
        }
        
        private void BindGameSignals()
        {
            Container.DeclareSignal<EndLevelMapInitializeSignal>();
            Container.DeclareSignal<OnPlayLevelButtonClickSignal>();
            Container.DeclareSignal<ComingSoonSignal>();
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
            Container.DeclareSignal<OnUpdateUiValuesSignal>();
            Container.DeclareSignal<OnInitShopItemsSignal>();
            Container.DeclareSignal<OnShopElementClickSignal>();
            Container.DeclareSignal<OnShopPanelsOpenSignal>();
            Container.DeclareSignal<OnShopItemBuyClick>();
            Container.DeclareSignal<OnSetDefaultItemSignal>();
            Container.DeclareSignal<OnShopPanelCloseSignal>();
            Container.DeclareSignal<DoLockShopItemSignal>();
            Container.DeclareSignal<OnUpdateGoldAfterPurchaseSignal>();
        }
        
    }
}