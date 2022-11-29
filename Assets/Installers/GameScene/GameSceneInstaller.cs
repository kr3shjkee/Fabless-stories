using Signals.Ui;
using Ui;
using UnityEngine;
using Zenject;

namespace Installers.GameScene
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameUiPanelsController gameUiPanelsControllerPrefab;
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
        private void InstallLevelBindings()
        {
            
        }
        
        private void BindLevelSignals()
        {
        
        }
    }
}