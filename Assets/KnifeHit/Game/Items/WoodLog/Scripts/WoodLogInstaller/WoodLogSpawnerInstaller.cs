using UnityEngine;
using Zenject;

public class WoodLogSpawnerInstaller : MonoInstaller
{
    [SerializeField] WoodLogSpawner woodLogSpawner;
    public override void InstallBindings()
    {
        Container.Bind<WoodLogSpawner>()
            .FromInstance(woodLogSpawner)
            .AsSingle();
    }
}