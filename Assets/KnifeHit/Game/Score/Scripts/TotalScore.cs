using UnityEngine;

public class TotalScore : MonoBehaviour
{
    public int maxKnives { get; private set; }
    public int maxStages { get; private set; }

    public int appleCounter { get; private set; }

    private void Awake()
    {
        LoadScore();
    }

    public void TrySaveScore(int knives, int stages, int apples)
    {
        if (knives>maxKnives) PlayerPrefs.SetInt("maxKnives",knives);
        if (stages > maxStages) PlayerPrefs.SetInt("maxStages", stages);
        if (apples > appleCounter) PlayerPrefs.SetInt("appleCounter", apples);
        LoadScore();
    }

    private void LoadScore()
    {
        maxKnives = PlayerPrefs.GetInt("maxKnives");
        maxStages = PlayerPrefs.GetInt("maxStages");
        appleCounter = PlayerPrefs.GetInt("appleCounter");
    }

    private void ResetTotalScore()
    {
        PlayerPrefs.SetInt("maxKnives", 0);
        PlayerPrefs.SetInt("maxStages", 0);
        PlayerPrefs.SetInt("appleCounter", 0);
    }
}
