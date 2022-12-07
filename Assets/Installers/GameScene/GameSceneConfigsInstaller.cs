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
        public override void InstallBindings()
        {
            Container.BindInstance(chapterConfigs);
            Container.BindInstance(chapterMapConfigs);
        }
    }
}