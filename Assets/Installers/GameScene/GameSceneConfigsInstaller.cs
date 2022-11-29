using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSceneConfigsInstaller", menuName = "Installers/GameSceneConfigsInstaller")]
public class GameSceneConfigsInstaller : ScriptableObjectInstaller<GameSceneConfigsInstaller>
{
    //[SerializeField] private ShopConfig
    public override void InstallBindings()
    {
        //Container.BindInstances(projectSettings);
    }
}