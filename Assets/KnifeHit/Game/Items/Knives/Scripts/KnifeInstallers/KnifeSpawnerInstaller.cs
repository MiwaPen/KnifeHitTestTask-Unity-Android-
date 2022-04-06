using UnityEngine;
using Zenject;

public class KnifeSpawnerInstaller : MonoInstaller
{
    [SerializeField] private KnifeSpawnerController _knifeSpawner;
    public override void InstallBindings()
    {
        Container.Bind<KnifeSpawnerController>()
            .FromInstance(_knifeSpawner)
            .AsSingle();
    }
}