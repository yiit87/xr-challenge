using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool Pause { get; private set; }
    public GameObject PauseMenu;

    public bool PlayerDead { get; private set; }
    public bool LevelComplete { get; private set; }
    public Text ScoreText;
    private float TotalScore { get; set; }

    private int HealthValue;
    public Image HealthTotalRepresenter;

    private int StarCollect;
    public Text StarCollectedText;

    public GameObject WarningNotEnoughStar;

    private float Timer = 5f;

    GameObject Player;
    private bool switchTheGameState = false;
    public AudioSource MainMusic;

    public GameObject YouDiedPanel;
    public GameObject EndGamePanel;

    public EventSystem eventS;

    public GameObject FirstSelected_Options;
    public GameObject FirstSelected_Dead;
    public GameObject FirstSelected_LevelComplete;

    private const float TOTAL_SCORE = 0f;
    private const int HEALTH = 100;
    private const string TAG_PLAYER = "Player";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Pause = false;
        TotalScore = TOTAL_SCORE;
        HealthValue = HEALTH;
        HealthBarAdjuster();
        PlayerDeadCondition(false);
        Player = GameObject.FindGameObjectWithTag(TAG_PLAYER);


       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            
            OptionsMenuButton();
        }

        PauseConditions();
        UpdateScoreValue();
        UpdateCollectedStarValue();

        WarningTextShut();
    }
    
    public void PauseTheGame(bool value)
    {
        Pause = value;
    }


    public void PlayerDeadCondition(bool value)
    {
        if (value == true && PlayerPrefs.HasKey("Xbox"))
        {
            if (MainMusic.isPlaying)
            {
                MainMusic.Pause();
            }
            eventS.SetSelectedGameObject(FirstSelected_Dead);
            eventS.firstSelectedGameObject = FirstSelected_Dead;         
        }

        PlayerDead = value;
    }
  
    private void PauseConditions()
    {
        if (Pause)
        {
            if (MainMusic.isPlaying)
            {
                MainMusic.Pause();
            }
            PauseMenu.SetActive(true);
        }
        else
        {
            if (!MainMusic.isPlaying)
            {
                MainMusic.UnPause();
            }
            PauseMenu.SetActive(false);
        }
    }
    
    public void AddToTotalScore(float value)
    {
        TotalScore += value;
    }   

    public void PlayerTakeDamage(int damage)
    {
        HealthValue -= damage;
        HealthBarAdjuster();
    }

    public void PlayerAddHealth(int heal)
    {
        HealthValue += heal;

        HealthBarAdjuster();
    }

    public void PlayerSpeedIncrease(int value)
    {
        Player.GetComponent<PlayerMovement>().AdjustSpeed(value);
    }

    public void AddCollectedStar()
    {
        StarCollect++;
    }

    public int Return_CollectedStar()
    {
        return StarCollect;
    }

    public void UpdateCollectedStarValue()
    {
        StarCollectedText.text = "Collected Stars: " + StarCollect.ToString() + "/5";
    }

    public void HealthBarAdjuster()
    {
        HealthTotalRepresenter.fillAmount = HealthValue / 100f;
    }
    public void UpdateScoreValue()
    {
        ScoreText.text = "Score: " + TotalScore.ToString();
    }

    public void WarningTextActivate()
    {
        WarningNotEnoughStar.SetActive(true);
    }

    public void WarningTextShut()
    {
        if (WarningNotEnoughStar.activeSelf)
        {
            Timer -= 1 * Time.deltaTime;

            if (Timer <= 0)
            {
                WarningNotEnoughStar.SetActive(false);
                Timer = 5f;
            }
        }
    }

    public void PlayerDiedPanelActivation()
    {
        YouDiedPanel.SetActive(true);
    }

    public void ActivateEndGamePanel()
    {
        EndGamePanel.SetActive(true);
        if (PlayerPrefs.HasKey("Xbox"))
        {
            eventS.SetSelectedGameObject(FirstSelected_LevelComplete);
            eventS.firstSelectedGameObject = FirstSelected_LevelComplete;
        }
    }

    //Menu Button Functionalities
    public void OptionsMenuButton()
    {
        switchTheGameState = !switchTheGameState; // Change between true or false
        PauseTheGame(switchTheGameState); // Depending of the true or false value pause or unpause the game
        PauseMenu.SetActive(switchTheGameState); // Depending of the true or false value turn on or off the pause menu

        if (PlayerPrefs.HasKey("Xbox"))
        {
            eventS.SetSelectedGameObject(FirstSelected_Options);
            eventS.firstSelectedGameObject = FirstSelected_Options;
        }
    }
   
    #region Button Functionalities
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//next level
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeButton()
    {
        OptionsMenuButton();
    }

    public void ExitButton()
    {
        Application.Quit();
    }
    #endregion

    public void SetLevelCompleteBool()
    {
        LevelComplete = true;
    }

    
}
