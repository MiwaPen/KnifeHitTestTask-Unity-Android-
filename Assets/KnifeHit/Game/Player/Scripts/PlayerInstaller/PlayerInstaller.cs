using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerController playerController;
    public override void InstallBindings()
    {
        Container.Bind<PlayerController>()
            .FromInstance(playerController)
            .AsSingle();
    }
}