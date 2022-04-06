using UnityEngine;
using Zenject;

public class SceneController : MonoBehaviour
{
    [SerializeField] private LevelScreenController levelScreen;
    [SerializeField] private LoseScreenBehaviour loseScreen;
    [SerializeField] private MenuController menuController;
    [Inject] private LevelBehaviour levelBehaviour;
    [Inject] private LevelScore levelScore;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        levelBehaviour.OnLose += ShowScoreScreen;
        loseScreen.OnRestartBTNClick += StartLevel;
        loseScreen.OnMenuBTNClick += ShowMainMenu;
        menuController.OnStartBTNClick += StartLevel;
        menuController.OnExitBTNClick += Exit; 
    }

    private void Start()
    {
        levelScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(false);
        menuController.gameObject.SetActive(false);
        ShowMainMenu();
    }
    private void StartLevel()
    {
        levelScreen.gameObject.SetActive(true);
        loseScreen.gameObject.SetActive(false);
        menuController.gameObject.SetActive(false);

        levelBehaviour.ResetLevelConfig();
        levelBehaviour.LoadStage();
    }

    private void ShowMainMenu()
    {
        menuController.gameObject.SetActive(true);
        levelScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(false);
    }

    private void ShowScoreScreen()
    {
        levelScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(true);
        menuController.gameObject.SetActive(false);
        loseScreen.LoadLoseScreenInfo();
        levelScore.ResetRunScore();
    }

    private void Exit()
    {
        Application.Quit();
    }
}
