using GameUi;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSceneConfigsInstaller", menuName = "Installers/GameSceneConfigsInstaller")]
public class GameSceneConfigsInstaller : ScriptableObjectInstaller<GameSceneConfigsInstaller>
{
    [SerializeField] private ChapterDialogConfig[] chapterDialogConfig;
    public override void InstallBindings()
    {
        Container.BindInstances(chapterDialogConfig);
    }
}