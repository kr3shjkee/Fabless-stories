using Configs;
using UnityEngine;
using Zenject;

namespace Installers.GameScene
{
    [CreateAssetMenu(fileName = "GameSceneConfigsInstaller", menuName = "Installers/GameSceneConfigsInstaller")]
    public class GameSceneConfigsInstaller : ScriptableObjectInstaller<GameSceneConfigsInstaller>
    {
        [SerializeField] private ChapterConfig chapterConfigs;
        [SerializeField] private ChapterMapConfig[] chapterMapConfigs;
        [SerializeField] private ShopItemConfig shopItemConfig;
        public override void InstallBindings()
        {
            Container.BindInstances(chapterConfigs, chapterMapConfigs, shopItemConfig);
        }
    }
}