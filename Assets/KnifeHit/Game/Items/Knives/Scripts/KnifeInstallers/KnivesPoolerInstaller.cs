using UnityEngine;
using Zenject;

public class KnivesPoolerInstaller : MonoInstaller
{
    [SerializeField] private KnivesPooler knivesPooler;
    public override void InstallBindings()
    {
        Container.Bind<KnivesPooler>()
            .FromInstance(knivesPooler)
            .AsSingle()
            .NonLazy();
    }
}