using UnityEngine;
using System;
using Zenject;

public class LevelScore : MonoBehaviour
{
    [Inject] private TotalScore totalScore;
    public int TotalKnifeCounter { get; private set; }
    public int TotalStageCounter { get; private set; }
    public int TotalAppleCounter { get; private set; }

    public Action OnScoreChange;

    private void Start()
    {
        ResetRunScore();
    }
    public void ResetRunScore()
    {
        TotalKnifeCounter = 0;
        TotalStageCounter = 1;
        TotalAppleCounter = totalScore.appleCounter;
        OnScoreChange?.Invoke();
    }
    public void IncreesKnifeCounter()
    {
        TotalKnifeCounter++;
        OnScoreChange?.Invoke();
    }

    public void IncreesAppleCounter(int appleValue)
    {
        TotalAppleCounter+= appleValue;
        OnScoreChange?.Invoke();
    }

    public void IncreesStageCounter()
    {
        TotalStageCounter++;
        OnScoreChange?.Invoke();
    }
}
