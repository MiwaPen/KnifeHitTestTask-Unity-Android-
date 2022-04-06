using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;
using TMPro;
using Cysharp.Threading.Tasks;

public class MenuController : MonoBehaviour
{
    public Action OnStartBTNClick;
    public Action OnExitBTNClick;
    [Inject] private TotalScore totalScore;
    [SerializeField] private Image fadeIMG;
    [SerializeField] private TMP_Text knivesCounter;
    [SerializeField] private TMP_Text appleCounter;
    [SerializeField] private TMP_Text stageCounter;
    [SerializeField] private Button playBTN;
    [SerializeField] private Button exitBTN;

    private void Awake()
    {
        playBTN.onClick.AddListener(StartBTNClick);
        exitBTN.onClick.AddListener(ExitBTNClick);
    }

    private void OnEnable()
    {
        playBTN.interactable = true;
        exitBTN.interactable = true;
        fadeIMG.enabled = true;
        fadeIMG.DOFade(1, 0f).OnComplete(() =>
        {
            
            fadeIMG.DOFade(0, 1f).OnComplete(() =>
            {
                fadeIMG.enabled = false;
            });
            
        });

        LoadScoreInfo();
    }

    private void DisableBTNS()
    {
        playBTN.interactable = false;
        exitBTN.interactable = false;
    }

    private async void StartBTNClick()
    {
        DisableBTNS();
        DoFade();
        await UniTask.Delay(1000);
        OnStartBTNClick?.Invoke();
    }

    private async void ExitBTNClick()
    {
        DisableBTNS();
        DoFade();
        await UniTask.Delay(1000);
        OnExitBTNClick?.Invoke();
    }

    private void DoFade()
    {
        fadeIMG.DOKill(true);
        fadeIMG.enabled = true;
        fadeIMG.DOFade(1, 1.1f).OnComplete(() =>
        {
            fadeIMG.enabled = false;
        });
    }

    private void LoadScoreInfo()
    {
        knivesCounter.text = "Knives " + totalScore.maxKnives;
        stageCounter.text = "Stage " + totalScore.maxStages;
        appleCounter.text = totalScore.appleCounter + "";
    }
}
