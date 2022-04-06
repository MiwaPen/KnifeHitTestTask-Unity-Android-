using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private LevelBehaviour levelBehaviour;
    public override void InstallBindings()
    {
        Container.Bind<LevelBehaviour>()
            .FromInstance(levelBehaviour)
            .AsSingle()
            .NonLazy();
    }
}