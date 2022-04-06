using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class LevelBehaviour : MonoBehaviour
{
    public Action OnLose;

    [Inject] private PlayerController player;
    [Inject] private TotalScore totalScore;
    [Inject] private LevelScore levelScore;
    [Inject] private KnivesPooler knivesPooler;
    [Inject] private KnifeSpawnerController _knifeSpawner;
    [Inject] private WoodLogSpawner _logSpawner;
    [SerializeField] private LevelInfo levelInfo;
    [SerializeField] private LevelScreenController levelScreen;

    private int _currentKnifeCount;
    private float _currentSpeedRotation;


    public void LoadStage()
    {
       
        _logSpawner.TrySpawnNewWoodLog(_currentKnifeCount,_currentSpeedRotation);
        knivesPooler.Initialize();
        _knifeSpawner.SetMaxCount(_currentKnifeCount);
        _knifeSpawner.EnableKnifeSpawner();
        player.EnablePlayerController();
        levelScreen.UpdateKnifeBar();
    }

    public void StageWin()
    {
        UpStageSettings();
        LoadNextStage();
    }
    private async void LoadNextStage()
    {
        ResetLevel();
        int nextStageDelay = 200;
        await UniTask.Delay(nextStageDelay);
        LoadStage();
    }

    public void ResetLevel()
    {
        player.DisablePlayerController();
        _knifeSpawner.DisableKnifeSpawner();
        knivesPooler.DisableAllItems();
        _logSpawner.DestroyCurrentLog();
    }

    public void ResetLevelConfig()
    {
        _currentKnifeCount = levelInfo.LevelStartKnivesCount;
        _currentSpeedRotation = levelInfo.LevelStartLogSpeed;
    }

    private void UpStageSettings()
    {
        if (_currentKnifeCount < levelInfo.LevelMaxKnivesCount) 
            _currentKnifeCount++;
        if (_currentSpeedRotation>levelInfo.LevelMaxLogSpeed) 
            _currentSpeedRotation -= levelInfo.RotationChangeStep;
    }

    public async void LoseGame()
    {
        ResetLevelConfig();
        TrySaveLevelScore();
        player.DisablePlayerController();
        _knifeSpawner.DisableKnifeSpawner();
        await UniTask.Delay(500);
        knivesPooler.DisableAllItems();
        _logSpawner.DestroyCurrentLog();
        OnLose?.Invoke();
    }

    private void TrySaveLevelScore()
    {
        totalScore.TrySaveScore(levelScore.TotalKnifeCounter,
            levelScore.TotalStageCounter,
            levelScore.TotalAppleCounter);
    }
}
