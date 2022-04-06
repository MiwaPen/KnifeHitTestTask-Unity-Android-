using UnityEngine;
using Zenject;

public class WoodLogSpawner : MonoBehaviour
{
    public WoodLogBehaviourScript _woodLogPrefab;

    [Inject] private LevelScore _levelScore;
    [Inject] private LevelBehaviour _levelBehaviour;
    [Inject] private DiContainer diContainer;
    private Transform _transform;
    private WoodLogBehaviourScript _currentLog = null;

    private void Awake()
    {
        _transform = this.transform;
    }

    public void DestroyCurrentLog()
    {
        if (_currentLog == null) return;
        _currentLog.OnBreakWoodLog -= _levelScore.IncreesStageCounter;
        _currentLog.OnBreakWoodLog -= _levelBehaviour.StageWin;
        _currentLog.OnKnifeEnterWood -= _levelScore.IncreesKnifeCounter;
        _currentLog.DestroyLog();
    }

    public void TrySpawnNewWoodLog(int logHp, float rotationDuration)
    {
        GameObject newLog = diContainer.InstantiatePrefab(_woodLogPrefab, _transform);
        _currentLog = newLog.GetComponent<WoodLogBehaviourScript>();
        _currentLog.transform.position = _transform.position;
        _currentLog.rotationDuration = rotationDuration;
        _currentLog.StartRotate();
        _currentLog.logHP = logHp;
        _currentLog.OnBreakWoodLog += _levelScore.IncreesStageCounter;
        _currentLog.OnBreakWoodLog += _levelBehaviour.StageWin;
        _currentLog.OnKnifeEnterWood += _levelScore.IncreesKnifeCounter;
        AppleBehaviour apple = _currentLog.TrySpawnApple();
        if (apple != null)
            apple.OnCutApple += _levelScore.IncreesAppleCounter;
    }
}
