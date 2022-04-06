using UnityEngine;
using Zenject;

public class TotalScoreInstaller : MonoInstaller
{
    [SerializeField] private TotalScore totalScore;
    public override void InstallBindings()
    {
        Container.Bind<TotalScore>()
            .FromInstance(totalScore)
            .AsSingle()
            .NonLazy();
    }
}