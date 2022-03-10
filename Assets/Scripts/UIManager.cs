using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameStates state;

    [SerializeField] private Text score;
    [SerializeField] private Text life;
    [SerializeField] private Text AchievementText;

    [SerializeField] private GameObject achievementWindow;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endGameAPanel;
    [SerializeField] private GameObject dontDiePanel;

    public delegate void GameState(GameStates state);
    public static event GameState changeGameState;

    private float achievementWindowTimer = 3f;
    private float _achievementWindowTimer;

    //Achievement Images
    [SerializeField] private Image appleImage;
    [SerializeField] private Image bananaImage;
    [SerializeField] private Image melonImage;
    [SerializeField] private Image enemiesImage;
    [SerializeField] private Image explosionImage;
    [SerializeField] private Image tutorialImage;

    #region CodeTutorial
    [SerializeField] private GameObject screenShotImage;
    [SerializeField] private RawImage screenshot;
    #endregion
    void Awake()
    {
        PlayerStats.addToUIScore += AddToScore;                     //subscriben 
        PlayerStats.addToUILife += AddToLife;                       //subscriben
        PlayerStats.onDie += ActivateDontDiePanel;                  //subscriben
        AchievementManager.unlockAchievement += ShowAchievement;    //subscriben 
        PlayerController.changeGameState += ChangeUIState;          //subscriben

        TutorialEvent.onStartTutorial += SetTutorialImage;
        TutorialEvent.onEndTutorial += DeactivateAllPanels;

        EndGoal.onReached += ActivateEndgamePanel;
    }

    
    void Update()
    {
        if(_achievementWindowTimer > 0)
        {
            _achievementWindowTimer -= Time.deltaTime;
            if (_achievementWindowTimer <= 0) CloseAchievementWindow();
        }
    }

    void AddToLife(int value)
    {
        this.life.text = "Life " + value;
    }

    void AddToScore(int value)
    {
        this.score.text = "Score " + value;
    }

    void ShowAchievement(AchievementType type)
    {
        _achievementWindowTimer = achievementWindowTimer;
        achievementWindow.SetActive(true);
        AchievementText.text = "You have achieved the " + type + " achievement";

        switch (type)
        {
            case AchievementType.apple:
                appleImage.color = new Color(appleImage.color.r, appleImage.color.g, appleImage.color.b, 1f);
                break;
            case AchievementType.banana:
                bananaImage.color = new Color(bananaImage.color.r, bananaImage.color.g, bananaImage.color.b, 1f);
                break;
            case AchievementType.melon:
                melonImage.color = new Color(melonImage.color.r, melonImage.color.g, melonImage.color.b, 1f);
                break;
            case AchievementType.enemy:
                enemiesImage.color = new Color(enemiesImage.color.r, enemiesImage.color.g, enemiesImage.color.b, 1f);
                break;
            case AchievementType.explosion:
                explosionImage.color = new Color(explosionImage.color.r, explosionImage.color.g, explosionImage.color.b, 1f);
                break;
            case AchievementType.tutorial:
                tutorialImage.color = new Color(tutorialImage.color.r, tutorialImage.color.g, tutorialImage.color.b, 1f);
                break;
        }
    }
    void CloseAchievementWindow()
    {
        achievementWindow.SetActive(false);
    }
    
    public void PauseBackButton()
    {
        ChangeUIState(GameStates.Playing);
    }

    public void ChangeUIState(GameStates newState)
    {
        DeactivateAllPanels();
        switch (newState)
        {
            case GameStates.Playing:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                changeGameState(GameStates.Playing);
                break;
            case GameStates.Pause:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseMenu.SetActive(true);
                break;
        }
        state = newState;
    }

    private void DeactivateAllPanels()
    {
        pauseMenu.SetActive(false);
        screenShotImage.SetActive(false);
    }

    private void SetTutorialImage(Texture screenShot)
    {
        Debug.Log(screenShot);
        this.screenshot.texture = screenShot;
        screenShotImage.SetActive(true);
    }

    private void ActivateEndgamePanel()
    {
        endGameAPanel.SetActive(true);
    }

    private void ActivateDontDiePanel()
    {
        dontDiePanel.SetActive(true);
        StartCoroutine(DeactivateSinglePanel(dontDiePanel));
    }

    IEnumerator DeactivateSinglePanel(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        obj.SetActive(false);
    }
}

public enum UIStates
{
    Playing,
    Pause,
}
