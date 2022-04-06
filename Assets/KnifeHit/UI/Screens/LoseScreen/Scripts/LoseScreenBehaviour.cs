using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;

public class LoseScreenBehaviour : MonoBehaviour
{
    public Action OnRestartBTNClick;
    public Action OnMenuBTNClick;
    [Inject] private LevelScore levelScore;
    [Inject] private SoundController soundController;
    [SerializeField] private Button restartBTN;
    [SerializeField] private Button menuBTN;
    [SerializeField] private TMP_Text totalKnivesCounter;
    [SerializeField] private TMP_Text totalStagesCounter;
    [SerializeField] private TMP_Text totalAppleCounter;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        restartBTN.onClick.AddListener(() => { OnRestartBTNClick?.Invoke(); DisableBTNS(); });
        menuBTN.onClick.AddListener(() => { OnMenuBTNClick?.Invoke(); DisableBTNS(); });
    }
    private void DisableBTNS()
    {
        restartBTN.interactable = false;
        menuBTN.interactable = false;
    }

    private  void OnEnable()
    {
        restartBTN.interactable = true;
        menuBTN.interactable = true;
        soundController.PlaySound(soundController.LoseSound);
        canvasGroup.DOFade(0, 0f)
            .OnComplete(() => {
             canvasGroup.DOFade(1, 2f);
            });
    }

    public void LoadLoseScreenInfo()
    {
        totalKnivesCounter.text = levelScore.TotalKnifeCounter + "";
        totalStagesCounter.text = "Stage "+ levelScore.TotalStageCounter;
        totalAppleCounter.text = levelScore.TotalAppleCounter + "";
    }
}
