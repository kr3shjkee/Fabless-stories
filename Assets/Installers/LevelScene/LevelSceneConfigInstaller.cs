using Configs;
using Level;
using UnityEngine;
using Zenject;

namespace Installers.LevelScene
{
    [CreateAssetMenu(fileName = "LevelSceneConfigInstaller", menuName = "Installers/LevelSceneConfigInstaller")]
    public class LevelSceneConfigInstaller : ScriptableObjectInstaller<LevelSceneConfigInstaller>
    {
        [SerializeField] private ElementConfig elementsConfig;
        [SerializeField] private BoardConfig boardConfig;
        [SerializeField] private LevelConfigs levelConfigs;
        [SerializeField] private ShopItemConfig shopItemConfig;
        public override void InstallBindings()
        {
            Container.BindInstances(elementsConfig, boardConfig, levelConfigs, shopItemConfig);
        }
    }
}