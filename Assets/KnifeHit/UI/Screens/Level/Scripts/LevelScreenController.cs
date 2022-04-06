using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using DG.Tweening;

public class LevelScreenController : MonoBehaviour
{
    [Inject] private LevelScore levelScore;
    [Inject] private KnifeSpawnerController knifeSpawner;
    [SerializeField] private TMP_Text knivesCounter;
    [SerializeField] private TMP_Text appleCounter;
    [SerializeField] private TMP_Text stageCounter;
    [SerializeField] private GameObject knivesBarItem;
    [SerializeField] private RectTransform knivesBar;
    [SerializeField] private Image fadeIMG;
    [SerializeField] private SpriteRenderer fadeSprite;
    private List<GameObject> knivesBarList = new List<GameObject>();
    private int knivesStock;
    private int TotalKnifeCount;

    private void OnEnable()
    {
        fadeSprite.enabled = true;
        fadeSprite.DOFade(1, 0f).OnComplete(() =>
        {

            fadeSprite.DOFade(0, 1f).OnComplete(() =>
            {
                fadeSprite.enabled = false;
            });

        });

        fadeIMG.enabled = true;
        fadeIMG.DOFade(1, 0f).OnComplete(() =>
        {

            fadeIMG.DOFade(0, 1f).OnComplete(() =>
            {
                fadeIMG.enabled = false;
            });

        });
        levelScore.OnScoreChange += UpdateLevelUI;
        knifeSpawner.OnKnivesCounterChange += UpdateLevelUI;
       
        UpdateLevelUI();
    }
    private void OnDisable()
    {
        levelScore.OnScoreChange -= UpdateLevelUI;
        knifeSpawner.OnKnivesCounterChange -= UpdateLevelUI;
    }

    private void UpdateLevelUI()
    {
        knivesCounter.text = levelScore.TotalKnifeCounter + "";
        appleCounter.text = levelScore.TotalAppleCounter + "";
        stageCounter.text = "Stage "+levelScore.TotalStageCounter;
        UpdateKnifeBarInfo();
    }

    public void UpdateKnifeBar()
    {
        float itemPos = 0;
        if (knivesBarList.Count > 0)
        {
            for (int i = 0; i < knivesBarList.Count; i++)
            {
                Destroy(knivesBarList[i]);
            }
            knivesBarList.Clear();
        }

        TotalKnifeCount = knifeSpawner._knifeCount;
        knivesStock = TotalKnifeCount;
        for (int i = 0; i < knivesStock; i++)
        {
            GameObject item = Instantiate(knivesBarItem, knivesBar);
            item.GetComponent<RectTransform>().localPosition = new Vector3(0,itemPos,0);
            itemPos += 100;
            knivesBarList.Add(item);
        }
    }

    private void UpdateKnifeBarInfo()
    {
        if(knivesBarList.Count <= 0) return;
        knivesStock = knifeSpawner._knifeCount;
     
        for (int i = knivesBarList.Count-1; i > knivesStock-1; i--)
        {
            knivesBarList[i].GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }      
    }
}
