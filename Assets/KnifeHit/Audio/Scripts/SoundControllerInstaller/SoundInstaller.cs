using UnityEngine;
using Zenject;

public class SoundInstaller : MonoInstaller
{
    [SerializeField] private SoundController soundController;
    public override void InstallBindings()
    {
        Container.Bind<SoundController>()
            .FromInstance(soundController)
            .AsSingle();
    }
}