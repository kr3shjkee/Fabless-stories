using Common;
using Configs;
using DebugMenu;
using GameUi;
using Level;
using LevelUi;
using Signals.DebugMenu;
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
        [SerializeField] private ShopItem shopItemPrefab;
        [SerializeField] private LevelDebugMenu levelDebugMenuPrefab;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BoardController>().AsSingle().NonLazy();
            Container.BindFactory<ElementConfigItem, ElementPosition, Element, Element.Factory>()
                .FromComponentInNewPrefab(elementPrefab);
            Container.BindInterfacesAndSelfTo<LevelUiManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle().NonLazy();
            Container.Bind<LevelUiPanelsController>().FromComponentInNewPrefab(levelUiPanelsController).AsSingle().NonLazy();
            Container.BindFactory<ItemConfig, ShopItem, ShopItem.Factory>()
                .FromComponentInNewPrefab(shopItemPrefab);
            Container.BindInterfacesAndSelfTo<ShopController>().AsSingle().NonLazy();
            
            Container.Bind<LevelDebugMenu>().FromComponentInNewPrefab(levelDebugMenuPrefab).AsSingle().NonLazy();
            
            BindLogicSignals();
            BindUiSignals();
            BindDebugSignals();
        }
    
        private void BindLogicSignals()
        {
            Container.DeclareSignal<OnElementClickSignal>();
            Container.DeclareSignal<OnBoardMatchSignal>();
            Container.DeclareSignal<OnRestartSignal>();
            Container.DeclareSignal<OnStepsChangedSignal>();
            Container.DeclareSignal<OnElementMatchShowSignal>();
            Container.DeclareSignal<OnDoStepSignal>();
            Container.DeclareSignal<OnBackStepSignal>();
            Container.DeclareSignal<OnHealthChangedSignal>();
            Container.DeclareSignal<OnStepsChangedSignal>();
            Container.DeclareSignal<OnBackStepsChangedSignal>();
            Container.DeclareSignal<OnGoldChangedSignal>();
            Container.DeclareSignal<OnBackStepsRestoredSignal>();
            Container.DeclareSignal<OnHealthRestoreSignal>();
            Container.DeclareSignal<OnTargetsChangedSignal>();
            Container.DeclareSignal<OnStepsRestoredSignal>();
            Container.DeclareSignal<OnLevelCompleteSignal>();
            Container.DeclareSignal<OnLeaveSceneSignal>();
        }

        private void BindUiSignals()
        {
            Container.DeclareSignal<OnCloseCurrentPanelSignal>();
            Container.DeclareSignal<OnExitButtonClickSignal>();
            Container.DeclareSignal<OnHealthButtonClickSignal>();
            Container.DeclareSignal<OnShopButtonClickSignal>();
            Container.DeclareSignal<OnOptionsButtonClickSignal>();
            Container.DeclareSignal<OnSoundOptionsButtonClickSignal>();
            Container.DeclareSignal<OnBackStepsButtonClickSignal>();
            Container.DeclareSignal<OnUpdateUiValuesSignal>();
            Container.DeclareSignal<OnShopPanelsOpenSignal>();
            Container.DeclareSignal<OnShopItemBuyClick>();
            Container.DeclareSignal<OnSetDefaultItemSignal>();
            Container.DeclareSignal<OnShopPanelCloseSignal>();
            Container.DeclareSignal<DoLockShopItemSignal>();
            Container.DeclareSignal<OnUpdateGoldAfterPurchaseSignal>();
            Container.DeclareSignal<OnInitShopItemsSignal>();
            Container.DeclareSignal<OnShopElementClickSignal>();
            Container.DeclareSignal<OnHealthBuyButtonClick>();
        }
        
        private void BindDebugSignals()
        {
            Container.DeclareSignal<OnHealthDownSignal>();
            Container.DeclareSignal<OnHealthFullSignal>();
            Container.DeclareSignal<OnGoldZeroSignal>();
            Container.DeclareSignal<OnGoldRichSignal>();
            Container.DeclareSignal<OnCloseDebugClick>();
            Container.DeclareSignal<OnCheckHealthTimerSignal>();
            Container.DeclareSignal<OnLevelCompleteDebugSignal>();
            Container.DeclareSignal<OnSetLastStepSignal>();
        }
    }
}