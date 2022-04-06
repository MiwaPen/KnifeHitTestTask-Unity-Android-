using UnityEngine;
using Zenject;

public class LevelScoreInstaller : MonoInstaller
{
    [SerializeField] private LevelScore _levelScore;
    public override void InstallBindings()
    {
        Container.Bind<LevelScore>()
            .FromInstance(_levelScore)
            .AsSingle();
    }
}